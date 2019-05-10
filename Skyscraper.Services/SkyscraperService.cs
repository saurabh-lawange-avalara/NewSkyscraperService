using Avalara.Skyscraper.Data;
using Avalara.Skyscraper.Data.Dapper.Entities;
using Avalara.Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalara.Skyscraper.Services
{
    public class SkyscraperService : ISkyscraperService
    {
        private ISkyscraperDataHelper _skyscraperDataHelper;

        public SkyscraperService(ISkyscraperDataHelper helper)
        {
            _skyscraperDataHelper = helper;
        }

        public ServiceModel_JobOwnerAccountInfo GetAccountInfoFromJobData(long jobId)
        {
            var jobData = _skyscraperDataHelper.GetAccountInfoFromJobData(jobId);
            return new ServiceModel_JobOwnerAccountInfo
            {
                AccountId = jobData.AccountId,
                ClientApiKeyId = (int)jobData.ClientApiKeyId
            };
        }

        public List<ServiceModel_ApiStatus> GetApiStatus(string apiName, string method)
        {
            var apiStatus = _skyscraperDataHelper.GetApiStatus(apiName, method);
            return apiStatus.Select(e => new ServiceModel_ApiStatus
            {
                ApiName = e.ApiName,
                Id = e.Id,
                Method = e.Method,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate,
                Status = e.Status
            }).ToList();
        }

        public ServiceModel_ClientAPIKeys GetClientAPIKeyObject(string apiKey)
        {
            var apikeyobj = _skyscraperDataHelper.GetClientAPIKeyByKey(apiKey);
            if (apikeyobj != null)
            {
                //Return status if job found
                return new ServiceModel_ClientAPIKeys()
                {
                    Id = apikeyobj.Id,
                    APIKey = apikeyobj.APIKey,
                    AppName = apikeyobj.AppName,
                    DepartmentId = (int)apikeyobj.DepartmentId,
                    ExpirationDate = apikeyobj.ExpirationDate,
                    ModifiedDate = apikeyobj.ModifiedDate
                };
            }
            else
            {
                return null;
            }
        }

        public ServiceModel_UserRole GetRoleId(int deptId, string username)
        {
            var user = _skyscraperDataHelper.GetRoleId(deptId, username);
            return new ServiceModel_UserRole
            {
                DepartmentId = (int)user.DepartmentId,
                Id = user.Id,
                RoleId = (int)user.RoleId,
                SkyscraperUserId = (int)user.SkyscraperUserId,
                UserName = username,
                
            };
        }

        public ServiceModel_SkyscraperUser GetSkyscraperUser(string userName)
        {
            var skyscraperUser = _skyscraperDataHelper.GetSkyscraperUser(userName);
            return new ServiceModel_SkyscraperUser
            {
                UserName = skyscraperUser.UserName,
                SkyscraperUserId = (int)skyscraperUser.SkyscraperUserId,
                AISubjectId = skyscraperUser.AISubjectId,
                AvaTaxAccountId = skyscraperUser.AvaTaxAccountId,
                AvaTaxUserId = skyscraperUser.AvaTaxUserId,
                AvaTaxUserRoleId = skyscraperUser.AvaTaxUserRoleId,
                CreateDateTime = skyscraperUser.CreateDateTime,
                FirstName = skyscraperUser.FirstName,
                LastLoginDateTime = skyscraperUser.LastLoginDateTime,
                UpdateDateTime = skyscraperUser.UpdateDateTime,
                LastName = skyscraperUser.LastName
            };
          
        }

        public long InsertSkyscraperUser(ServiceModel_SkyscraperUser user)
        {
            SkyscraperUser skyscraperUser = new SkyscraperUser
            {
                UserName = user.UserName,
                SkyscraperUserId = user.SkyscraperUserId,
                AvaTaxAccountId = user.AvaTaxAccountId,
                AISubjectId = user.AISubjectId,
                AvaTaxUserId = user.AvaTaxUserId,
                AvaTaxUserRoleId = user.AvaTaxUserRoleId,
                FirstName = user.FirstName,
                CreateDateTime = user.CreateDateTime,
                LastLoginDateTime = user.LastLoginDateTime,
                LastName = user.LastName,
                UpdateDateTime = user.UpdateDateTime
            };
            return _skyscraperDataHelper.InsertSkyscraperUser(skyscraperUser);
        }

        public int InsertUserRole(ServiceModel_UserRole role)
        {
            var userRole = new UserRoles()
            {
                DepartmentId = role.DepartmentId,
                Id = role.Id,
                RoleId = role.RoleId,
                SkyscraperUserId = role.SkyscraperUserId,
            };

            return _skyscraperDataHelper.InsertUserRole(userRole);
        }

        public void UpdateSkyscraperuser(ServiceModel_SkyscraperUser user)
        {
            SkyscraperUser skyscraperUser = new SkyscraperUser
            {
                UserName = user.UserName,
                SkyscraperUserId = user.SkyscraperUserId,
                AvaTaxAccountId = user.AvaTaxAccountId,
                AISubjectId = user.AISubjectId,
                AvaTaxUserId = user.AvaTaxUserId,
                AvaTaxUserRoleId = user.AvaTaxUserRoleId,
                FirstName = user.FirstName,
                CreateDateTime = user.CreateDateTime,
                LastLoginDateTime = user.LastLoginDateTime,
                LastName = user.LastName,
                UpdateDateTime = user.UpdateDateTime                
            };
            _skyscraperDataHelper.UpdateSkyscraperuser(skyscraperUser);
        }
    }
}
