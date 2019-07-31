using Avalara.Skyscraper.Data.Dapper.Entities;
using Avalara.Skyscraper.Data.DapperExtensionModels;
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
        List<JobStatusInfo> GetJobStatuses(string jobIdList);
        List<SSResource> GetImagesByJob(long jobId);
        WebFileData GetWebFileData(long jobId);
        List<FormMetaData> GetFormStatus(string taxformcode, string JobTypeClause);
        List<PaymentInfoAdditionalFields> GetPaymentInfoRequiredFields(IEnumerable<string> scraperRegions);
    }
}
