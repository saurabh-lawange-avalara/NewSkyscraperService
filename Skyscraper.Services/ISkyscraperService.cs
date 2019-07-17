using Avalara.Skyscraper.Models;
using System;
using System.Collections.Generic;

namespace Avalara.Skyscraper.Services
{
    public interface ISkyscraperService
    {
        #region apistatus
        List<ApiStatusResponseModel> GetApiStatus(string apiName, string method);
        #endregion

        #region auth helpers
        UserRoleModel GetRoleId(int deptId, string username);
        void UpdateSkyscraperuser(SkyscraperUserModel user);
        long InsertSkyscraperUser(SkyscraperUserModel user);
        int InsertUserRole(UserRoleModel role);
        SkyscraperUserModel GetSkyscraperUser(string userName);
        JobOwnerAccountInfoModel GetAccountInfoFromJobData(long jobId);
        ClientAPIKeysModel GetClientAPIKeyObject(string apiKey);
        #endregion

        #region webfile
        WebFileResponseModel GetWebFile(long jobId);
        List<JobStatusMessageModel> GetJobStatuses(string jobIds);
        List<SkyScraperResourceModel> GetImagesByJob(long jobId);
        #endregion
    }
}
