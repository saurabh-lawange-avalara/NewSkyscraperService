using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace Avalara.Skyscraper.Web.Common
{
    public class AIAuthorizeFilter : IActionFilter
    {
        private readonly IAuthenticate _authenticate;
        private string bearerToken;

        public AIAuthorizeFilter(IAuthenticate authenticate) 
        {
            _authenticate = authenticate;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            try
            {                
                IAuthenticationInfo authInfo = null;

                if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                {
                    return;
                }
                if (context.HttpContext.Request.Headers.Any(e => e.Key.Equals("bearertoken", StringComparison.OrdinalIgnoreCase)))
                {
                    bearerToken = context.HttpContext.Request.Headers.First(e => e.Key.Equals("bearertoken", StringComparison.OrdinalIgnoreCase)).Value.FirstOrDefault();
                    authInfo = AuthenticateBearerToken();
                }
                else
                {
                    var credentials = context.HttpContext.Request.Headers.ContainsKey("Authorization") ? context.HttpContext.Request.Headers["Authorization"] : context.HttpContext.Request.Headers["authorization"];
                    if (!String.IsNullOrEmpty(credentials))
                    {
                        string credential = credentials.ToString();
                        if (credential.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                        {
                            //Validating Credentials using Account Service Rest V2
                            authInfo = _authenticate.GetBasicAuthenticatedUser(credential);
                        }
                        else if (credential.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            bearerToken = credential.Replace("Bearer ", string.Empty).Trim();
                            authInfo = AuthenticateBearerToken();
                        }
                    }
                }

                if (authInfo != null)
                {
                    _authenticate.SetAuthenticatedUserInContext(authInfo, context.HttpContext);
                    return;
                }

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.HttpContext.Response.Headers.Clear();
                context.Result = new JsonResult("Unauthorized");
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private IAuthenticationInfo AuthenticateBearerToken()
        {
            // Validating BearerToken using Account Service Rest V2
            IAuthenticationInfo authInfo = _authenticate.ValidateAccessKeyUsingRestV2(bearerToken);
            if (authInfo == null)
            {
                //Validating BearerToken using AI
                authInfo = _authenticate.ValidateAccessKey(bearerToken);
            }            
            return authInfo;
        }
    }
}
