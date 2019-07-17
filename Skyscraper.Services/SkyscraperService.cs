using Avalara.Skyscraper.Common;
using Avalara.Skyscraper.Data;
using Avalara.Skyscraper.Data.Dapper.Entities;
using Avalara.Skyscraper.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avalara.Skyscraper.Services
{
    public class SkyscraperService : ISkyscraperService
    {
        #region properties
        private ISkyscraperDataHelper _skyscraperDataHelper;
        private ILog _logger;
        #endregion

        #region constructor
        public SkyscraperService(ISkyscraperDataHelper helper, ILog logger)
        {
            _skyscraperDataHelper = helper;
            _logger = logger;
        }
        #endregion

        #region apistatus helpers
        public List<ApiStatusResponseModel> GetApiStatus(string apiName, string method)
        {
            var apiStatus = _skyscraperDataHelper.GetApiStatus(apiName, method);
            return apiStatus.Select(e => new ApiStatusResponseModel
            {
                ApiName = e.ApiName,
                Id = e.Id,
                Method = e.Method,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate,
                Status = e.Status
            }).ToList();
        }
        #endregion

        #region auth helpers
        public JobOwnerAccountInfoModel GetAccountInfoFromJobData(long jobId)
        {
            var jobData = _skyscraperDataHelper.GetAccountInfoFromJobData(jobId);
            return new JobOwnerAccountInfoModel
            {
                AccountId = jobData.AccountId,
                ClientApiKeyId = (int)jobData.ClientApiKeyId
            };
        }

        public ClientAPIKeysModel GetClientAPIKeyObject(string apiKey)
        {
            var apikeyobj = _skyscraperDataHelper.GetClientAPIKeyByKey(apiKey);
            if (apikeyobj != null)
            {
                //Return status if job found
                return new ClientAPIKeysModel()
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

        public UserRoleModel GetRoleId(int deptId, string username)
        {
            var user = _skyscraperDataHelper.GetRoleId(deptId, username);
            return new UserRoleModel
            {
                DepartmentId = (int)user.DepartmentId,
                Id = user.Id,
                RoleId = (int)user.RoleId,
                SkyscraperUserId = (int)user.SkyscraperUserId,
                UserName = username,
                
            };
        }

        public SkyscraperUserModel GetSkyscraperUser(string userName)
        {
            var skyscraperUser = _skyscraperDataHelper.GetSkyscraperUser(userName);
            return new SkyscraperUserModel
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

        public long InsertSkyscraperUser(SkyscraperUserModel user)
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

        public int InsertUserRole(UserRoleModel role)
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

        public void UpdateSkyscraperuser(SkyscraperUserModel user)
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
        #endregion

        #region webfile helpers
        /// <summary>
        /// Fetch one web file response
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns> 
        public WebFileResponseModel GetWebFile(long jobId)
        {
            try
            {
                var jobStatuses = GetJobStatuses(jobId.ToString());
                JobStatusMessageModel jobStatus = null;
                if (jobStatuses != null && jobStatuses.Count > 0)
                {
                    jobStatus = jobStatuses.FirstOrDefault();
                }
                if (jobStatus == null || jobStatus.JobType != Models.JobType.WEBFILE)
                {
                    return null;
                }

                var response = new WebFileResponseModel()
                {
                    Mode = jobStatus.Mode,
                    JobId = jobStatus.JobId,
                    Message = jobStatus.Message,
                    ProcessedOnDateTime = jobStatus.UpdateDateTime,
                    Status = jobStatus.Status.ToString(),
                    Error = jobStatus.Error
                };

                //this method will seperated pipe seperated values in message for actual error and login account type and assign the value sin respective fields.
                ResolveResponseMessageAndLoginType(response);

                //return the response here if Job is yet to complete.
                if (!CommonHelper.IsJobComplete(jobStatus.Status.ToString()))
                {
                    return response;
                }

                //Now get images
                var imageList = GetImagesByJob(jobStatus.JobId);

                if (imageList != null && imageList.Count > 0)
                {
                    //Convert internal SSResource to simpler Image object.
                    response.Images = imageList.Select(e => new ImageModel()
                    {
                        ImageId = e.SSResourceId,
                        Name = e.Name,
                        Comments = e.Comments,
                        Tags = e.Tags,
                        CreateDateTime = e.CreateDateTime,
                        UpdateDateTime = e.UpdateDateTime
                    }).ToArray();

                    response.Confirmations = CommonHelper.FindConfirmations(imageList).Select(e => new ImageModel()
                    {
                        ImageId = e.SSResourceId,
                        Name = e.Name,
                        Comments = e.Comments,
                        Tags = e.Tags,
                        CreateDateTime = e.CreateDateTime,
                        UpdateDateTime = e.UpdateDateTime
                    }).ToArray();
                }

                var webFileData = GetWebFileData(jobId);
                if (webFileData != null && !string.IsNullOrEmpty(webFileData.FilingData))
                {
                    response.FilingData = JsonConvert.DeserializeObject<Dictionary<string, object>>(webFileData.FilingData);
                    throw new Exception("Manual Exception ");
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new Exception(string.Format("Error Retrieving job: {0}", ex.Message));
            }
        }

        public List<JobStatusMessageModel> GetJobStatuses(string jobIds)
        {
            var result = _skyscraperDataHelper.GetJobStatuses(jobIds);
            var items = new List<JobStatusMessageModel>();
            if (result != null)
            {
                result.ForEach(e =>
                {
                    JobStatusMessageModel job = new JobStatusMessageModel()
                    {
                        JobId = e.JobId,
                        JobType = (Models.JobType)e.JobTypeId,
                        Status = (Models.JobStatus)e.JobStatusId,
                        Message = e.Message,
                        InternalMessage = e.InternalMessage,
                        QueueDateTime = e.QueueDateTime,
                        UpdateDateTime = e.UpdateDateTime ?? DateTime.MinValue,
                        JobStartDateTime = e.JobStartDateTime ?? DateTime.MinValue,
                        Error = string.IsNullOrEmpty(e.Error) ? null : JsonConvert.DeserializeObject<SkyScraperErrorModel>(e.Error),
                        Mode = e.Mode
                    };
                    items.Add(job);
                });

                return items;
            }
            return null;
        }
        
        public List<SkyScraperResourceModel> GetImagesByJob(long jobId)
        {
            //sanity checks:
            //  verify job id
            var images = _skyscraperDataHelper.GetImagesByJob(jobId);

            //Removing download from S3. This data is not needed in this result.
            //images.ForEach(image => 
            //{
            //    if (!string.IsNullOrEmpty(image.FileKey))
            //    {
            //        image.ImageData = _s3Helper.DownloadFile(image.FileKey, s3FilePath);
            //    }
            //});

            return images.Select(e => new SkyScraperResourceModel()
            {
                Comments = e.Comments,
                CreateDateTime = e.CreateDateTime,
                FileKey = e.FileKey,
                FileType = e.FileType,
                JobId = e.JobId,
                Name = e.Name,
                SSResourceId = e.SSResourceId,
                UpdateDateTime = e.UpdateDateTime,
                Tags = e.Tags,
                ImageData = e.ImageData
            }).ToList();
        }

        public WebFileDataModel GetWebFileData(long jobId)
        {
            var webFileData = _skyscraperDataHelper.GetWebFileData(jobId);
            return new WebFileDataModel()
            {
                JobId =webFileData.JobId,
                CreateDateTime = webFileData.CreateDateTime,
                FilingData = webFileData.FilingData
            };
        }
        #endregion

        #region private methods
        private void ResolveResponseMessageAndLoginType(WebFileResponseModel jobResponse)
        {
            //Message in jobstatus will have pipe seperated values.part[0] will have actual message returned by website and 
            //part[1] which will have account type.hence spliting on pipe "|".
            string[] tempMessageAndLoginType = null;

            if (string.IsNullOrEmpty(jobResponse.Message))
            {
                return;
            }

            tempMessageAndLoginType = jobResponse.Message.Split('|');
            jobResponse.Message = tempMessageAndLoginType.Length > 0 ? tempMessageAndLoginType[0] : null;
            jobResponse.LoginAccountType = tempMessageAndLoginType.Length > 1 ? tempMessageAndLoginType[1] : null;

        }

        #endregion
    }
}
