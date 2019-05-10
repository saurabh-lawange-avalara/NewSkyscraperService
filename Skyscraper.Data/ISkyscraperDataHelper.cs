using Avalara.Skyscraper.Data.Dapper.Entities;
using System;
using System.Collections.Generic;

namespace Avalara.Skyscraper.Data
{
    public interface ISkyscraperDataHelper
    {
        List<ApiStatus> GetApiStatus(string apiName, string method);
        UserRoles GetRoleId(int deptId, string username);
        void UpdateSkyscraperuser(SkyscraperUser user);
        long InsertSkyscraperUser(SkyscraperUser user);
        int InsertUserRole(UserRoles role);
        SkyscraperUser GetSkyscraperUser(string userName);
        JobData GetAccountInfoFromJobData(long jobId);
        ClientAPIKeys GetClientAPIKeyByKey(string apikey);
    }
}
