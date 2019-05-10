using Avalara.Authentication;
using Avalara.AvaTax.RestClient;
using Avalara.Skyscraper.Web.Common.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Common
{
    public class AvalaraIdentityHelper : IAuthenticate
    {
        private readonly IConfiguration _configuration;
        private readonly AvaTaxClient avaTaxClient;

        public AvalaraIdentityHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            Uri uri = new Uri(_configuration.GetSection("AuthenticationServices")["AvalaraAccountSvc"]);
            avaTaxClient = new AvaTaxClient("Skyscraper Svc", "4.0", Environment.MachineName, uri);            
        }

        /// <summary>
        /// AI auth.
        /// </summary>
        /// <param name="bearerToken"></param>
        /// <returns></returns>
        public IAuthenticationInfo ValidateAccessKey(string bearerToken)
        {
            AuthenticationInfo authInfo = null;
            try
            {
                UserInfoResponse response = null;

                using (var clientHandler = new HttpClientHandler())
                {
                    Uri uri = new Uri(_configuration.GetSection("AuthenticationServices")["AvalaraIdentity"]);
                    var userInfoClient = new UserInfoClient(new Uri(uri, "connect/userinfo").ToString(), clientHandler);
                    response = userInfoClient.GetAsync(bearerToken).Result;

                    if (response.IsError)
                    {
                        return new AuthenticationInfo
                        {
                            AuthenticationType = "BearerToken",
                            BearerToken = bearerToken,
                            IsAuthenticated = false
                        };
                    }
                }

                AvalaraIdentityUser user = response.Json.ToObject<AvalaraIdentityUser>();

                authInfo = new AuthenticationInfo
                {
                    Name = user.name,
                    IsAuthenticated = true,
                    AuthenticationType = "BearerToken",
                    BearerToken = bearerToken
                };

                int id;
                if (!string.IsNullOrEmpty(user.avatax_user_id) && int.TryParse(user.avatax_user_id, out id))
                {
                    authInfo.AvaTaxUserId = id;
                }

                if (!string.IsNullOrEmpty(user.avatax_account_id) && int.TryParse(user.avatax_account_id, out id))
                {
                    authInfo.AvaTaxAccountId = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return authInfo;
        }

        public IAuthenticationInfo ValidateAccessKeyUsingRestV2(string bearerToken)
        {
            AuthenticationInfo authInfo = null;
            try
            {
                if (!string.IsNullOrEmpty(bearerToken))
                {
                    Uri uri = new Uri(_configuration.GetSection("AuthenticationServices")["AvalaraAccountSvc"]);
                    var client = avaTaxClient.WithBearerToken(bearerToken);

                    var pingResult = client.Ping();
                    if (pingResult.authenticated.Value)
                    {
                        authInfo = new AuthenticationInfo
                        {
                            AuthenticationType = "BearerToken",
                            AvaTaxUserId = pingResult.authenticatedUserId,
                            AvaTaxAccountId = pingResult.authenticatedAccountId,
                            IsAuthenticated = true,
                            Name = pingResult.authenticatedUserName,
                            BearerToken = bearerToken
                        };
                    }
                    else
                    {
                        authInfo = new AuthenticationInfo
                        {
                            AuthenticationType = "BearerToken",
                            IsAuthenticated = false
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error("exception caugth while authenticating using AvataxClient rest v2" + ex.Message);
            }
            return authInfo;
        }

        public IAuthenticationInfo GetBasicAuthenticatedUser(string authHeader)
        {
            IAuthenticationInfo authInfo = null;
            try
            {
                if (!string.IsNullOrEmpty(authHeader))
                {
                    //Parse auth header string to get user credentials and then get the user entity from db.
                    var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
                    if (authHeaderVal != null)
                    {
                        var parts = Encoding.UTF8.GetString(Convert.FromBase64String(authHeaderVal.Parameter)).Split(':');
                        if (string.IsNullOrEmpty(parts[0]) || string.IsNullOrEmpty(parts[1]))
                        {
                            return null;
                        }

                        AvaTaxClient client = avaTaxClient.WithSecurity(username: parts[0].ToLower(), password: parts[1]);
                        var pingResult = client.Ping();
                        if (pingResult.authenticated.Value)
                        {
                            authInfo = new AuthenticationInfo
                            {
                                AuthenticationType = "Basic Authentication",
                                AvaTaxUserId = pingResult.authenticatedUserId,
                                AvaTaxAccountId = pingResult.authenticatedAccountId,
                                IsAuthenticated = true,
                                Name = pingResult.authenticatedUserName                               
                            };
                        }
                        else
                        {
                            authInfo = new AuthenticationInfo
                            {
                                AuthenticationType = "Basic Authentication",
                                IsAuthenticated = false
                            };
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Log.Error("Exception while validating credentials: " + ex);
                //If exception is not related to non-authenticated user. Something else like connectivity, then throw the original error. Else consume it here.
                if (ex.Message.IndexOf("could not be authenticated", StringComparison.OrdinalIgnoreCase) == -1 &&
                    ex.Message.IndexOf("401", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    throw;
                }

            }
            return authInfo;

        }

        public void SetAuthenticatedUserInContext(IAuthenticationInfo userInfo, HttpContext context)
        {
            var client = avaTaxClient;
            var avataxUserDetails = client.GetUserAsync(userInfo.AvaTaxUserId.Value, userInfo.AvaTaxAccountId.Value, "*").Result;

            UserEntity userEntity = new UserEntity()
            {
                AccessLevelId = 4,
                AccountId = userInfo.AvaTaxAccountId.Value,
                IsActive = userInfo.IsAuthenticated,
                UserId = userInfo.AvaTaxUserId.Value,
                UserName = userInfo.Name,
                SecurityRoleId = (byte)avataxUserDetails.securityRoleId
            };

            context.User = new GenericPrincipal(userInfo, new string[0]);
            context.Items.Add("UserBearerToken", userInfo.BearerToken);
            context.SetAvaUser(userEntity);
            //AddUserEntityToCache(userEntity);
            AuthHelper.InsertUpdateSkyscraperUser(userEntity);            
        }

    }
}
