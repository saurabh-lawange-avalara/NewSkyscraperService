using Avalara.Skyscraper.Models;
using System;
using System.Collections.Generic;

namespace Avalara.Skyscraper.Services
{
    public interface ISkyscraperService
    {
        List<ServiceModel_ApiStatus> GetApiStatus(string apiName, string method);
        ServiceModel_UserRole GetRoleId(int deptId, string username);
        void UpdateSkyscraperuser(ServiceModel_SkyscraperUser user);
        long InsertSkyscraperUser(ServiceModel_SkyscraperUser user);
        int InsertUserRole(ServiceModel_UserRole role);
        ServiceModel_SkyscraperUser GetSkyscraperUser(string userName);
        ServiceModel_JobOwnerAccountInfo GetAccountInfoFromJobData(long jobId);
        ServiceModel_ClientAPIKeys GetClientAPIKeyObject(string apiKey);
    }
}
