using Avalara.Authentication;
using Avalara.Returns.Data;
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
        JobCreateResponse SetAndCreateWebFileJob(WebFileModel request, UserEntity user, List<FormStatusResponse> formMetaData, int deptid, int clientApiKeyId);
        List<JobStatusMessageModel> GetJobStatuses(string jobIds);
        List<SkyScraperResourceModel> GetImagesByJob(long jobId);
        ComputedFormData GetCFDFromS3(string fileKey);
        bool ValidatePaymentInfoKeys(dynamic payment, string skyScraperRegion, out string validationMsg);
        #endregion
        #region form
        List<FormStatusResponse> GetFormStatus(string taxformcode, int JobTypeId = 0);

        #endregion
    }
}
