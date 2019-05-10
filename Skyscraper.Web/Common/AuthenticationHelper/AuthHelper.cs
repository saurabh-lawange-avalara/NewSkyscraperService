using Avalara.Authentication;
using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalara.Skyscraper.Web.Common
{
    /// <summary>
    /// Helper class for auth module
    /// </summary>
    public class AuthHelper
    {
        private static IConfiguration _config;
        private static ISkyscraperService _svcHelper;
        public AuthHelper(IConfiguration configuration, ISkyscraperService skyscraperService)
        {
            _config = configuration;
            _svcHelper = skyscraperService;
        }

        public bool IsSkyscraperUser(int deptId)
        {
            int SkyscraperDeptId = Convert.ToInt32(_config["SkyscraperDepartmentId"]);
            return (SkyscraperDeptId == deptId);
        }

        public ServiceModel_UserRole GetRole(int deptId, string username)
        {
            var userRole = _svcHelper.GetRoleId(deptId, username);
            return userRole;
        }

        public List<string> GetSkyscraperSystemUser()
        {
            var systemUserNames = new List<string>();

            var usernamesStr = _config["RootUserNames"];
            
            //read system user names from config else use hardcoded ones.
            if (!string.IsNullOrEmpty(usernamesStr))
            {
                systemUserNames = usernamesStr.Split(new char[] { ',', ';', '|' }).ToList();
            }
            else
            {
                systemUserNames.Add("SKYSCRAPER.SYSTEMUSER.COMPLIANCE");
                systemUserNames.Add("SKYSCRAPER.SYSTEMUSER");
                systemUserNames.Add("SKYSCRAPER.ROOT.COMPLIANCE");
            }

            return systemUserNames;            
        }

        public ServiceModel_JobOwnerAccountInfo GetAccountInfoFromJobData(long jobId)
        {
            var jobOwnerInfo = _svcHelper.GetAccountInfoFromJobData(jobId);
            return jobOwnerInfo;
        }

        public ServiceModel_ClientAPIKeys GetClientAPIKeyObject(string apiKey)
        {
            return _svcHelper.GetClientAPIKeyObject(apiKey);
        }

        public void CreateUserAndRoleIfNotExist(ActionExecutingContext context, UserEntity user)
        {
            int skyscraoerUerId = (int)InsertUpdateSkyscraperUser(user);
            int deptId = Convert.ToInt32(context.HttpContext.Items["DepartmentId"].ToString());
            var roleInfo = GetRole(deptId, user.UserName);
            if (roleInfo == null)
            {
                ServiceModel_UserRole role = new ServiceModel_UserRole();
                role.RoleId = (int)Roles.BasicUser;
                role.SkyscraperUserId = skyscraoerUerId;
                role.UserName = user.UserName;
                role.DepartmentId = deptId;
                int id = CreateUserRole(role);
            }
        }

        public int CreateUserRole(ServiceModel_UserRole role)
        {
            return _svcHelper.InsertUserRole(role);
        }


        /// <summary>
        /// inset the new Skyscraper user or update existing one
        /// </summary>
        /// <param name="userEntity"></param>
        public static long InsertUpdateSkyscraperUser(UserEntity userEntity)
        {
            if (userEntity != null)
            {
                var skyscraperUser = _svcHelper.GetSkyscraperUser(userEntity.UserName);

                if (skyscraperUser == null)
                {
                    skyscraperUser = new ServiceModel_SkyscraperUser()
                    {
                        AvaTaxUserId = userEntity.UserId.ToString(),
                        AvaTaxUserRoleId = userEntity.SecurityRoleId,
                        CreateDateTime = DateTime.UtcNow,
                        LastLoginDateTime = DateTime.UtcNow,
                        UpdateDateTime = DateTime.UtcNow,
                        UserName = userEntity.UserName,
                        AvaTaxAccountId = userEntity.AccountId
                    };
                    return _svcHelper.InsertSkyscraperUser(skyscraperUser);
                }
                else
                {
                    skyscraperUser.LastLoginDateTime = DateTime.UtcNow;
                    _svcHelper.UpdateSkyscraperuser(skyscraperUser);
                    return skyscraperUser.SkyscraperUserId;
                }
            }
            return 0;
        }

    }
}
