using Avalara.Returns.AWSHelper;
using Avalara.Returns.Data;
using Avalara.Skyscraper.Common;
using Avalara.Skyscraper.Common.Extensions;
using Avalara.Skyscraper.Data;
using Avalara.Skyscraper.Data.Dapper.Entities;
using Avalara.Skyscraper.Model;
using Avalara.Skyscraper.Models;
using log4net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Avalara.Authentication;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Avalara.Skyscraper.Services
{
    public class SkyscraperService : ISkyscraperService
    {
        #region properties
        private ISkyscraperDataHelper _skyscraperDataHelper;
        private IS3Helper _s3Helper;
        private ILog _logger;
        private readonly SkyscraperSvcConfig _skyscraperSvcConfig;
        IMemoryCache _memoryCache;

        private const string passphrase = "E4tremelyHardT0Gue55W0rdHerE";
        private const string s3FilePath = "skyscraper";
        private const string s3UploadFilePath = "Skyscraper-UploadFiles";
        private const string s3JsonFilePath = "Skyscraper-RequestJson";
        #endregion

        #region constructor
        public SkyscraperService(ISkyscraperDataHelper helper, SkyscraperSvcConfig skyscraperSvcConfig, IS3Helper s3Helper, ILog logger, IMemoryCache memoryCache)
        {
            _skyscraperDataHelper = helper;
            _logger = logger;
            _s3Helper = s3Helper;
            _memoryCache = memoryCache;
            _skyscraperSvcConfig = skyscraperSvcConfig;
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
                JobId = webFileData.JobId,
                CreateDateTime = webFileData.CreateDateTime,
                FilingData = webFileData.FilingData
            };
        }

        public ComputedFormData GetCFDFromS3(string fileKey)
        {
            //gets cfd from s3 with reference file key.
            try
            {
                string returnsJson = GetReturnsDataFromS3(fileKey);
                var cfdObject = JsonConvert.DeserializeObject<ComputedFormData>(returnsJson);
                if (cfdObject == null)
                {
                    throw new Exception("Must provide CFD element.");
                }

                if (cfdObject.Header == null)
                {
                    throw new Exception("Must provide CFD element with Header.");
                }
                if (cfdObject.returnsData == null)
                {
                    throw new Exception("Must provide CFD element with returnsData.");
                }
                if (cfdObject.summary == null)
                {
                    throw new Exception("Must provide CFD element with summary.");
                }
                return cfdObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FormStatusResponse> GetFormStatus(string taxformcode, int JobTypeId = 0)
        {
            string JobTypeClause = "(SS.JobTypeId=2 or SS.JobTypeId=7)";
            if (JobTypeId > 0)
            {
                if (JobTypeId == (int)Models.JobType.WEBFILE)
                {
                    JobTypeClause = string.Format("SS.JobTypeId={0}", JobTypeId);
                }
                else if (JobTypeId == (int)Models.JobType.WEBUPLOAD)
                {
                    JobTypeClause = string.Format("SS.JobTypeId={0}", JobTypeId);
                }
            }

            var list = _skyscraperDataHelper.GetFormStatus(taxformcode, JobTypeClause).Select(
                    e =>
                        new FormStatusResponse
                        {
                            TaxFormId = e.TaxFormId,
                            TaxFormCode = e.TaxFormCode,
                            LegacyReturnName = e.LegacyReturnName,
                            Country = e.Country,
                            Region = e.Region,
                            ScraperRegion = e.ScraperRegion,
                            IsAvailable = (e.IsAvailable && e.ScraperStatus),
                            FileUpload = e.FileUpload,
                            BulkAccounts = (e.StrBulkAccounts != null) ? e.BulkAccounts : null,
                            FilingModes = e.FilingModes.Select(f => new PropertyDiscriptor() { Name = f.Name, Description = f.Description }).ToArray(),
                            PaymentModes = e.PaymentModes.Select(f => new PropertyDiscriptor() { Name = f.Name, Description = f.Description }).ToArray(),
                            RequiredFilingCalendarDataFields = e.RequiredFilingCalendarDataFields.Select(f => new PropertyDiscriptor() { Name = f.Name, Description = f.Description }).ToArray(),
                            IsBulkSupported = (e.WebfilingAccount & 2) > 0,
                            IsIndividualSupported = (e.WebfilingAccount & 1) > 0,
                            IsDefaultTaxForm = e.IsDefaultTaxForm,
                            FilingDisabledReason = e.FilingDisabledReason,
                            IsWebfileForm = e.IsWebfileForm,
                        }).ToList();

            return list;
        }

        #endregion

        #region private methods
        private string GetReturnsDataFromS3(string fileKeyPath, string jobType = null)
        {

            if (string.IsNullOrEmpty(fileKeyPath))
            {
                return null;
            }
            byte[] fileContent = null;

            fileContent = fileKeyPath.Contains("/") ? GetFileContentFromS3FilePath(fileKeyPath) : _s3Helper.DownloadFile(fileKeyPath, s3JsonFilePath);
            string json = Encoding.UTF8.GetString(fileContent);
            if (!string.IsNullOrEmpty(jobType) && Models.JobType.LOCATIONREGISTRATION.ToString().Equals(jobType))
            {
                return json;
            }
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (dictionary.ContainsKey("CFD"))
            {
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(dictionary["CFD"].ToString());
            }
            return JsonConvert.SerializeObject(dictionary);
        }

        private byte[] GetFileContentFromS3FilePath(string fileKeyPath, bool isUploadFile = false)
        {
            //this method gets upload file or cfd content from s3 from given s3 reference keys in request
            string source = isUploadFile ? "Upload File" : "CFD";

            try
            {
                string fileKey = string.Empty;
                string bucketName = GetBucketNameFromFileKey(fileKeyPath, out fileKey);
                if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(fileKey))
                {
                    throw new Exception(string.Format("S3 {0} file key reference format is invalid.Please send correct format.For assistance contact Skyscraper support.", source));
                }
                var newS3Helper = new S3Helper(bucketName);
                var fileContent = newS3Helper.DownloadFile(fileKey, string.Empty);
                return fileContent;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} => {1}", source, ex.Message));
            }
        }

        private string GetBucketNameFromFileKey(string fileKeyPath, out string fileKey)
        {
            //this method will extract out the bucket name and file key refernce and folder from a given s3 ref keys in request.
            try
            {
                fileKey = string.Empty;
                int index = 0;
                string[] arrayFileKeyPath = fileKeyPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                string bucketName = string.Empty;
                //gets bucket name from the given path.
                //first option stringformat "https://s3-us-west-2.amazonaws.com/abcde/RequestJson/abcd.JSON" => bucketname= abcde
                if (arrayFileKeyPath.Any(e => e.CompareValues("http") && arrayFileKeyPath.Any(c => c.CompareValues("s3,.com"))))
                {
                    if (arrayFileKeyPath.Length > 4)
                    {
                        index = Array.FindIndex(arrayFileKeyPath, e => e.CompareValues(".com")) + 1;
                        bucketName = arrayFileKeyPath[index];

                    }
                }
                else if (arrayFileKeyPath.Any(e => e.CompareValues("s3::"))) // "arn:aws:s3:::my_corporate_bucket/exampleobject.png"; bucketname= my_corporate_bucket
                {
                    if (arrayFileKeyPath.Length > 0)
                    {
                        index = Array.FindIndex(arrayFileKeyPath, e => e.CompareValues("s3:"));
                        string bucketNameContainer = arrayFileKeyPath[index];
                        bucketName = bucketNameContainer.Substring(bucketNameContainer.LastIndexOf(':') + 1).Trim();
                    }
                }

                /*
                 "//returns/skyscraper/sjdhksdsdh.json";
                 * "returns/skyscraper/sjdhksdsdh.json";
                 * "S3://returns/skyscraper/sjdhksdsdh.json";
                 * bucketName=returns
                 */
                else
                {
                    index = 0;
                    bucketName = arrayFileKeyPath[index];
                    if (bucketName.CompareValues("S3:"))
                    {
                        index = 1;
                        bucketName = arrayFileKeyPath[1];
                    }
                }


                //gets file key from the given path.
                foreach (var content in arrayFileKeyPath) //get fileKey 
                {
                    if (Array.IndexOf(arrayFileKeyPath, content) > index) //concat only indexes after bucket name index.
                    {
                        fileKey += content;
                        if (arrayFileKeyPath.Length != Array.IndexOf(arrayFileKeyPath, content) + 1)
                        {
                            fileKey += "/";
                        }
                    }

                }

                return bucketName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public bool ValidatePaymentInfoKeys(dynamic payment, string skyScraperRegion, out string validationMsg)
        {
            validationMsg = string.Empty;
            string supportedAccountTypes = string.Empty;
            PaymentInfoRequiredFields paymentInfo = JsonConvert.DeserializeObject<PaymentInfoRequiredFields>(JsonConvert.SerializeObject(payment));
            OrderedDictionary paymentInfoAdditionalFields = null;
            if (_memoryCache != null && _memoryCache.Get(skyScraperRegion + "_payment") != null)
            {
                //get payment required fields from cache
                paymentInfoAdditionalFields = (OrderedDictionary)_memoryCache.Get(skyScraperRegion + "_payment");
                //get supported accounttypes  from cache
                supportedAccountTypes = (string)_memoryCache.Get(skyScraperRegion + "_supportedAccountTypes");
            }
            else
            {
                //get from db and store in cache
                List<PaymentFields> paymentFields = GetPaymentAdditionalFields(new List<string>() { skyScraperRegion });
                paymentInfoAdditionalFields = GetPaymentSupportedAccountTypes(paymentFields, out supportedAccountTypes);
                if (paymentInfoAdditionalFields != null && paymentInfoAdditionalFields.Count > 0)
                {
                    _memoryCache.Set(skyScraperRegion + "_payment", paymentInfoAdditionalFields, DateTime.UtcNow.Add(TimeSpan.FromDays(1)));
                    _memoryCache.Set(skyScraperRegion + "_supportedAccountTypes", supportedAccountTypes, DateTime.UtcNow.Add(TimeSpan.FromDays(1)));
                }
            }

            #region basic bank info validation
            StringBuilder sb = new StringBuilder();
            var missingFields = new List<string>();
            //bank account number
            if (string.IsNullOrEmpty(paymentInfo.BankAccountNum))
            {
                missingFields.Add("BankAccountNum");
            }

            //bank routing number
            if (string.IsNullOrEmpty(paymentInfo.BankRoutingNum))
            {
                missingFields.Add("BankRoutingNum");
            }

            //account type
            if (string.IsNullOrEmpty(paymentInfo.AccountType))
            {
                missingFields.Add("AccountType");
            }

            //construct error
            if (missingFields.Count > 0)
            {
                sb.Append(string.Format("PaymentInfo must have values for following keys => {0}.", string.Join(",", missingFields)));
            }

            //payment date
            if (paymentInfo.PaymentDate == null)
            {
                sb.Append("PaymentInfo requires 'PaymentDate'.Please provide valid payment date on which payment to be made on DOR.");
            }
            //check if account type is supported
            if (!string.IsNullOrEmpty(paymentInfo.AccountType))
            {
                if (!supportedAccountTypes.Split('|').Contains(paymentInfo.AccountType, StringComparer.OrdinalIgnoreCase))
                {
                    sb.Append(string.Format("AccountType {0} not supported.Supported account types for this form => {1}. ", paymentInfo.AccountType, supportedAccountTypes));
                }
            }

            #endregion

            //additional fields if required
            object[] tempKeys = null;
            bool hasAdditionalFields = true;
            string[] optionalValuesKeys = null;
            if (paymentInfoAdditionalFields != null && paymentInfoAdditionalFields.Count > 0)
            {
                tempKeys = new object[paymentInfoAdditionalFields.Count];
                optionalValuesKeys = !string.IsNullOrEmpty(_skyscraperSvcConfig?.OptionalFields?.OptionalPaymentFields)
                    ? _skyscraperSvcConfig?.OptionalFields?.OptionalPaymentFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    : null;
                foreach (var item in paymentInfoAdditionalFields.Keys)
                {
                    if (paymentInfo.AdditionalFields == null || paymentInfo.AdditionalFields.Count < 1 || !paymentInfo.AdditionalFields.Contains(item) && !optionalValuesKeys.Contains(item) && string.IsNullOrEmpty((string)paymentInfo.AdditionalFields[item]))
                    {
                        hasAdditionalFields = false;
                        break;
                    }
                }
            }

            //if form requires additional payment fields  but not present
            if (!hasAdditionalFields)
            {
                paymentInfoAdditionalFields.Keys.CopyTo(tempKeys, 0);
                var strKeys = tempKeys.Select(e => (string)e).ToArray();
                sb.Append(string.Format("This Tax form requires  additional fields => {0} along with basic bank information in payment request object.Please provide values for each required keys.", string.Join(",", strKeys.Where(e => optionalValuesKeys == null || !optionalValuesKeys.Contains(e)))));
            }

            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                sb.Append("Refer to swagger documentation for Skyscraper v1 PaymentRequiredFields API to know more about payment info required fields.");
                validationMsg = sb.ToString();
                return false;
            }

            return true;
        }

        private List<PaymentFields> GetPaymentAdditionalFields(IEnumerable<string> skyScraperRegions)
        {
            var paymentFields = _skyscraperDataHelper.GetPaymentInfoRequiredFields(skyScraperRegions);
            if (paymentFields?.Count > 0)
            {
                return paymentFields.Select(e => new PaymentFields
                {
                    Country = e.Country,
                    FieldName = e.FieldName,
                    FieldValue = e.FieldValue,
                    MaskValue = e.MaskValue,
                    RegEx = e.RegEx,
                    ScraperRegion = e.ScraperRegion,
                    ShowValue = e.ShowValue.HasValue ? Boolean.Parse(e.ShowValue.ToString()) : false
                }).ToList();
            }
            else return null;
        }

        private OrderedDictionary GetPaymentSupportedAccountTypes(IEnumerable<PaymentFields> options, out string accountType, bool showValues = false)
        {
            accountType = null;
            var supportedAccountTypes = new List<string>();
            var additionalFieldsRegion = new OrderedDictionary();

            if (options != null && options.Count() > 0)
            {
                foreach (var item in options)
                {
                    if (showValues)
                    {
                        //paymentinfo api which gets info MRS customers should get values of all additional fields irrespective of the SHowValue flag in db.
                        item.ShowValue = true;
                    }
                    //add all account types in supported account types list and exclude them from adding in additional fields

                    if (item.FieldName.Equals("AccountType"))
                    {
                        supportedAccountTypes.Add(item.FieldValue);
                        continue;
                    }

                    // multiple values with similar keys should be pipe separated
                    if (additionalFieldsRegion.Contains(item.FieldName))
                    {
                        additionalFieldsRegion[item.FieldName] = additionalFieldsRegion[item.FieldName] + "|" + string.Format("V({0})", item.FieldValue);
                    }
                    else
                    {
                        //if item.ShowValue=true then display value for the field else mask it
                        additionalFieldsRegion.Add(item.FieldName, item.ShowValue ? string.Format("V({0})", item.FieldValue) : string.IsNullOrEmpty(item.RegEx) ? string.Format("M({0})", item.MaskValue) : string.Format("R({0})", item.RegEx));
                    }
                }

                if (supportedAccountTypes.Count > 0)
                {
                    //create a pipe separated values of account type and return.
                    accountType = string.Join("|", supportedAccountTypes);
                }

            }
            return additionalFieldsRegion;
        }
        public JobCreateResponse SetAndCreateWebFileJob(WebFileModel request, UserEntity user, List<FormStatusResponse> formMetaData, int deptid, int clientApiKeyId)
        {
            SkyscraperJobRequest job = new SkyscraperJobRequest();

            job.AccountId = user.AccountId;
            job.AvaTaxUserId = user.UserId;

            int companyid = 0;
            job.CompanyId = int.TryParse(request.CFD.Header.CompanyIdentifier, out companyid) ? companyid : 0;
            job.Country = formMetaData[0].Country.ToUpper();
            job.Region = formMetaData[0].ScraperRegion.ToUpper();
            //Worker Process expects these values to be present. We can remove it once on-demand compiling is implemented and there will be no need to pass command args.
            job.Username = request.Username == null ? "null" : request.Username;
            job.Password = request.Password == null ? "null" : request.Password;
            job.Mode = request.Mode.ToUpper().Trim();
            job.AdditionalOptions = (request.FilingCalendarData == null || request.FilingCalendarData.Count == 0) ? null : JsonConvert.SerializeObject(request.FilingCalendarData);
            // job.ReturnsDataJson = string.IsNullOrEmpty(request.S3CFDRefKey) ? JsonConvert.SerializeObject(request.CFD) : null;
            job.ReturnsDataFileKey = request.S3CFDRefKey;
            job.FilingYear = (short)request.CFD.Header.FilingYear;
            if (job.FilingYear <= 0)
            {
                _logger.Error("Filing Year must be a valid number.");
                return new JobCreateResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Filing Year must be a valid number."
                };
            }
            byte month = 0;
            if (byte.TryParse(request.CFD.Header.FilingMonth, out month))
            {
                job.FilingMonth = month;
            }
            else
            {
                _logger.Error("Filing month must be a valid number.");
                return new JobCreateResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Filing month must be a valid number."
                };
            }

            string[] updateFileNameRegion = _skyscraperSvcConfig.UpdateFileNameInRequest.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var updateFileNameInRequest = updateFileNameRegion.Where(c => c == job.Region).FirstOrDefault();

            if (updateFileNameInRequest != null && request.UploadFiles.Count > 0)
            {
                // Checking is state registration id is present in request or not , region : VA
                if (request.CFD.summary.Contains("StateRegistrationId") 
                    && !string.IsNullOrEmpty(request.CFD.summary["StateRegistrationId"].ToString()))
                {
                    CreateUploadFileName(request);
                }
                else
                {
                    _logger.Error("StateRegistrationId is missing in request->Summary.");
                    return new JobCreateResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "'StateRegistrationId' is missing in request."
                    };
                }
            }

            if (request.UploadFiles != null && request.UploadFiles.Count > 0)
            {
                //get files to be downloaded from s3
                var uploadFilesFromS3 = request.UploadFiles.Where(e => e.Content == null && !string.IsNullOrEmpty(e.FileKey)).ToList();

                //Get files with conent in request
                var uploadFilesWithContent = request.UploadFiles.Where(e => e.Content != null).ToList();

                //add request content files in job.Files
                if (uploadFilesWithContent != null && uploadFilesWithContent.Count > 0)
                {
                    job.Files = uploadFilesWithContent;
                }

                if (uploadFilesFromS3 != null && uploadFilesFromS3.Count > 0)
                {
                    //if request has upload file key reference.get the upload files from s3.
                    var listOfUploadFiles = GetUploadFilesFromS3(uploadFilesFromS3);
                    if (listOfUploadFiles != null && listOfUploadFiles.Count > 0)
                    {
                        //add in list files downloaded thorugh s3
                        if (job.Files == null)
                        {
                            job.Files = listOfUploadFiles;
                        }
                        else
                        {
                            job.Files.AddRange(listOfUploadFiles);
                        }

                        job.Files[0].Name = request.UploadFiles[0].Name;

                    }
                    else
                    {
                        _logger.Error("Invalid s3 uploadfile reference keys provided.");
                        return new JobCreateResponse
                        {
                            HttpStatusCode = HttpStatusCode.BadRequest,
                            Message = "Must provide detail of Uplaod file reference key info element with name and s3 file key."
                        };
                    }
                }
            }

            job.BulkRequestId = request.BulkRequestId;

            // Removing  return/filing due day check because now we started to use Payment date so the 'due day' is not mandatory 
            #region filing due day check
            //if (request.DueDay > 0)
            //{
            //    job.FilingDueDay = request.DueDay;
            //}
            //else
            //{
            //    _logger.Error("Must provide a valid Filing Due Day (number).");
            //    return GetApiResponse(HttpStatusCode.BadRequest, "Must provide a valid Filing Due Day (number).");
            //}
            #endregion

            job.FilingFrequencyId = CommonHelper.GetFilingFrequencyFromCode(request.CFD.Header.FilingFrequency);

            if (job.FilingFrequencyId == 0)
            {
                _logger.Error("Must provide valid filing frequency code");
                return new JobCreateResponse
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Must provide valid filing frequency code"
                };
            }

            job.TaxFormCode = request.CFD.Header.TaxFormCode;
            job.RefJobId = request.RefJobId;
            job.PaymentDetails = request.Payment;
            job.BulkAccountUserId = request.BulkAccount;
            job.ClientApiKeyId = clientApiKeyId;
            #region Test Request
            long testJobId; string errorMsg = string.Empty;
            bool isTestJob = CommonHelper.CheckIfTestJob(_skyscraperSvcConfig.TestJobs, request.Username, request.Password, request.Mode, job.JobType, out testJobId, out errorMsg);
            if (isTestJob)
            {
                if (testJobId < 1)
                {
                    _logger.Error(errorMsg);
                    return new JobCreateResponse
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = errorMsg
                    };
                }
                return new JobCreateResponse() { JobId = testJobId };
            }
            #endregion


            //check if file mode is allowed on this environment.
            if (string.IsNullOrEmpty(request.Mode) || (request.CFD.Header.CountryCode != "IND" && !CommonHelper.IsWebFileModeAllowed(request.Mode, _skyscraperSvcConfig.AllowedModesOnTest)))
            {
                _logger.Error("Mode not provided or not-allowed on this server");
                return new JobCreateResponse
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = "Mode not provided or not-allowed on this server"
                };
            }
            request.UploadFiles = null;
            request.Password = null;


            var jobResponse = _service.CreateNewWebFileJob(job, JsonConvert.SerializeObject(request.CFD));
            if (jobResponse.status != OperationStatus.SUCCESS)
            {
                _logger.Error(string.IsNullOrEmpty(jobResponse.Message) ? "Error creating job" : jobResponse.Message);
                return new JobCreateResponse
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = string.IsNullOrEmpty(jobResponse.Message) ? "Error creating job" : jobResponse.Message
                };
            }

            return new JobCreateResponse
            {
                HttpStatusCode = HttpStatusCode.OK,
                JobId = jobResponse.JobId
            };

        }

        private void CreateUploadFileName(WebFileModel request)
        {
            
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");

                var fileName = string.Format("{0}~{1}~{2}~{3}~{4}~{5}~confirmation", request.CFD.Header.TaxFormCode, request.CFD.Header.CompanyIdentifier,
                                 (request.CFD.Header.FilingMonth).ToString(), (request.CFD.Header.FilingYear).ToString("0000"),
                                  rgx.Replace(request.CFD.summary["StateRegistrationId"].ToString(), string.Empty), "Upload");

                //Update upload file name for webfile job
                
                    request.UploadFiles[0].Name = fileName + Path.GetExtension(request.UploadFiles[0].Name);                
            
        }

        private List<SkyscraperUploadFiles> GetUploadFilesFromS3(List<SkyscraperUploadFiles> fileKeys)
        {
            //gets the upload file content from s3 from given request.

            try
            {
                var files = new List<SkyscraperUploadFiles>();
                foreach (var item in fileKeys)
                {
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        throw new Exception("Name property missing in upload file element.");
                    }
                    var file = new SkyscraperUploadFiles();

                    file.Content = item.FileKey.Contains("/") ? GetFileContentFromS3FilePath(item.FileKey, true) : _s3Helper.DownloadFile(item.FileKey, s3UploadFilePath);

                    file.Name = item.Name;
                    file.FileKey = item.FileKey;
                    files.Add(file);
                }

                return files;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
