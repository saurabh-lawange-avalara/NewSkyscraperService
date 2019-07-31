using Avalara.Skyscraper.Data.Dapper.Entities;
using Avalara.Skyscraper.Data.DapperExtensionModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Avalara.Skyscraper.Data
{
    public class SkyscraperDataHelper : ISkyscraperDataHelper
    {
        private IDbConnectionFactory _connFactory;
        public SkyscraperDataHelper(IDbConnectionFactory connFactory)
        {
            _connFactory = connFactory;                   
        }

        public List<ApiStatus> GetApiStatus(string apiName, string method)
        {
            using (var dap = new ApiStatusDap(_connFactory.Connection))
            {
                var status = dap.GetAll(false).ToList();
                string whereClause = string.Empty;
                if (string.IsNullOrEmpty(apiName) && string.IsNullOrEmpty(method))
                {
                    return status;
                }
                else
                {
                    if (!string.IsNullOrEmpty(apiName) && !string.IsNullOrEmpty(method))
                    {
                        return status.Where(e => e.ApiName.Equals(apiName, StringComparison.OrdinalIgnoreCase) && e.Method.Equals(method, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else if (!string.IsNullOrEmpty(apiName))
                    {
                        return status.Where(e => e.ApiName.Equals(apiName, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    else
                    {
                        return status.Where(e => e.Method.Equals(method, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                }
            }
        }

        public UserRoles GetRoleId(int deptId, string username)
        {
            using (var dap = new UserRolesDap(_connFactory.Connection))
            {
                var param = new DynamicParameters();
                param.Add("@deptId", deptId);
                param.Add("@username", username);

                var result = dap.Query<UserRoles>("Select * from UserRoles UR join SkyscraperUser SU on UR.SkyscraperUserId=SU.SkyscraperUserId where UR.DepartmentId=@deptId and SU.UserName=@username", param).FirstOrDefault();
                return result;
            }
        }

        public void UpdateSkyscraperuser(SkyscraperUser user)
        {
            using (var dap = new SkyscraperUserDap(_connFactory.Connection))
            {
                dap.Update(user);
            }
        }

        public long InsertSkyscraperUser(SkyscraperUser user)
        {
            using (var dap = new SkyscraperUserDap(_connFactory.Connection))
            {
                return dap.InsertWithId(user);
            }
        }

        public int InsertUserRole(UserRoles role)
        {
            using (var dap = new RoleDap(_connFactory.Connection))
            {
                if (dap.GetById((int)role.RoleId) == null)
                {
                    return -1;
                }

                var departmentDap = new DepartmentDap(_connFactory.Connection);
                if (departmentDap.GetById((int)role.DepartmentId) == null)
                {
                    return -1;
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("@deptId", role.DepartmentId);
                param.Add("@userid", role.SkyscraperUserId);
                var result = dap.Query<UserRoles>("Select * from UserRoles where SkyscraperUserId=@userid and DepartmentId=@deptId", param).FirstOrDefault();


                if (result == null)
                {
                    result = new UserRoles();
                    result.SkyscraperUserId = role.SkyscraperUserId;
                    result.RoleId = role.RoleId;
                    result.DepartmentId = role.DepartmentId;
                    result.ModifiedDate = DateTime.UtcNow;
                    result.ExpirationDate = DateTime.MaxValue.AddYears(-1);
                    var dapUserRole = new UserRolesDap(_connFactory.Connection);
                    return dapUserRole.InsertWithId(result);
                }
                else
                {
                    return -2; //already exist use put to update
                }
            }
        }

        public SkyscraperUser GetSkyscraperUser(string userName)
        {
            using (var dap = new SkyscraperUserDap(_connFactory.Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);

                return dap.Query<SkyscraperUser>("SELECT * FROM SkyscraperUser WHERE UserName = @UserName", parameters).FirstOrDefault();
            };
        }

        public JobData GetAccountInfoFromJobData(long jobId)
        {
            using (var dap = new JobDataDap(_connFactory.Connection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@JobId", jobId);
                return dap.Query<JobData>("SELECT AccountId,ClientApiKeyId FROM JobData WHERE JobId = @JobId", parameters).FirstOrDefault();
            }
        }

        public ClientAPIKeys GetClientAPIKeyByKey(string apikey)
        {
            using (var dap = new ClientAPIKeysDap(_connFactory.Connection))
            {
                return dap.GetByApiKey(apikey);
            }
        }

        public List<JobStatusInfo> GetJobStatuses(string jobIdList)
        {
            var jobstatuses = new List<JobStatusInfo>();
            using (var dap = new JobStatusDap(_connFactory.Connection))
            {
                var param = new DynamicParameters();
                var result = dap.Query<JobStatusInfo>(" CALL `Skyscraper`.`procJobStatus`('" + jobIdList + "')", commandTimeout: 100);
                if (result != null)
                {
                    jobstatuses.AddRange(result);
                }

                if (result == null || result.Count() != jobIdList.Split(',').Length)
                {
                    //get jobstatus from archive tables if result is null or total number of response job id does not match requested JobIds 
                    result = dap.Query<JobStatusInfo>(" CALL `Skyscraper`.`procJobStatusArchive`('" + jobIdList + "')", commandTimeout: 100);
                    if (result != null)
                    {
                        jobstatuses.AddRange(result);
                    }
                }
                return jobstatuses;
            }
        }

        public List<SSResource> GetImagesByJob(long jobId)
        {
            using (var dap = new SSResourceDap(_connFactory.Connection))
            {
                //dap.GetByJobId(jobId);
                var parameters = new DynamicParameters();
                parameters.Add("@jobId", jobId);
                string sqlQuery = @"SELECT *,group_concat(TagName separator ', ') as Tags FROM Skyscraper.SSResource                                 
								left outer join Skyscraper.SSResourceTags on Skyscraper.SSResource.SSResourceId=Skyscraper.SSResourceTags.SSResourceId 
								left outer join Skyscraper.Tags on Skyscraper.SSResourceTags.TagId=Skyscraper.Tags.TagId 
								where Skyscraper.SSResource.jobId=@jobId  AND ( Skyscraper.SSResource.FileType = 0 OR  Skyscraper.SSResource.FileType IS NULL)
								group by  Skyscraper.SSResource.SSResourceId order by case  When Name like 'Step%' then cast(replace(replace(Name,'Step',''),'.png','') as unsigned)
                                else Skyscraper.SSResource.SSResourceId
                                end";
                var result = dap.Query<SSResource>(sqlQuery, parameters).ToList();

                if (result == null || result.Count == 0)
                {
                    //if result is null it means job id is in archive table
                    result = dap.Query<SSResource>(" CALL `Skyscraper`.`procImagesAndTagsArchive`('" + jobId.ToString() + "')", commandTimeout: 100).ToList(); // ds.Tables[0] != null ? ds.Tables[0].ToList<SSResource>() : null;
                }
                return result;
            }
        }

        public WebFileData GetWebFileData(long jobId)
        {
            using (var dap = new WebFileDataDap(_connFactory.Connection))
            {
                return dap.GetByJobId(jobId);
            }
        }

        #region form method handler
        /// <summary>
        /// Get status of the form in Skyscraper along with other important metadata required to file a particular 
        /// form through Skyscraper.
        /// </summary>
        /// <returns></returns>

        public List<FormMetaData> GetFormStatus(string taxformcode, string JobTypeClause)
        {            
            StringBuilder strQuery = new StringBuilder(string.Format("SET SESSION group_concat_max_len = 1000000; " +
                    "Select TF.TaxFormId, TF.TaxFormCode, TF.LegacyReturnName, TF.Country, TF.Region,TF.ScraperRegion, TF.IsWebFilingAvailable as IsAvailable, TF.FileUpload, TF.WebfilingAccount, TF.IsDefaultTaxForm, TF.FilingDisabledReason, TF.IsWebfileForm,  " +
                    "SS.IsAvailable as ScraperStatus, Group_Concat(TBA.BulkAccountId) as StrBulkAccounts, " +
                    "Group_Concat(TFFM.FilingModeId) as StrFilingModeIds,Group_Concat(TFPM.PaymentModeId) as StrPaymentModeIds, " +
                    "Group_Concat(TFRC.RequiredFilingCalendarDataFieldId) as StrCalenderFieldIds " +
                    "FROM TaxForm TF " +
                    "Join ScraperStatus SS on (SS.ScraperRegion = TF.ScraperRegion and SS.Country = TF.Country and {0}) " +
                    "Left Outer Join TaxFormBulkAccount TBA on TF.TaxFormId = TBA.TaxFormId " +
                    "Left outer join TaxFormFilingMode TFFM on TF.TaxFormId=TFFM.TaxFormId " +
                    "Left outer join TaxFormPaymentMode TFPM on TF.TaxFormId=TFPM.TaxFormId " +
                    "Left outer join TaxFormRequiredFilingCalendarDataField TFRC on TF.TaxFormId=TFRC.TaxFormId ", JobTypeClause));

            if (!string.IsNullOrEmpty(taxformcode))
            {
                strQuery.Append("Where TF.TaxFormCode = '" + taxformcode + "' ");
            }
            else
            {
                //get all webfiling forms
                strQuery.Append("Where TF.IsWebfileForm =1");
            }
            strQuery.Append(" Group by TF.Taxformcode,TF.ScraperRegion;");
            List<FormMetaData> list = new List<FormMetaData>();
            TaxFormDap dap = new TaxFormDap(_connFactory.Connection);
            list = dap.Query<FormMetaData>(strQuery.ToString()).ToList();
            

            BulkAccountDap badap = new BulkAccountDap(_connFactory.Connection);
            var allBulkAccts = badap.GetAll(false);

            FilingModeDap fmdap = new FilingModeDap(_connFactory.Connection);
            var allFilingModes = fmdap.GetAll(false);

            PaymentModeDap pmdap = new PaymentModeDap(_connFactory.Connection);
            var allPaymentModes = pmdap.GetAll(false);

            RequiredFilingCalendarDataFieldDap rcddap = new RequiredFilingCalendarDataFieldDap(_connFactory.Connection);
            var allCalendarFields = rcddap.GetAll(false);

            foreach (var taxForm in list)
            {
                try
                {
                    //bulkaccounts
                    if (!string.IsNullOrEmpty(taxForm.StrBulkAccounts))
                    {
                        var bulkAccountNameList = new List<string>();

                        foreach (var bulkAcctId in taxForm.StrBulkAccounts.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                        {
                            var bulkAccount = allBulkAccts.Where(e => e.BulkAccountId.ToString().Equals(bulkAcctId)).FirstOrDefault();
                            if (bulkAccount != null)
                            {
                                bulkAccountNameList.Add(bulkAccount.Username);
                            }
                        }
                        taxForm.BulkAccounts = bulkAccountNameList.ToArray();
                    }

                    //filing modes
                    if (!string.IsNullOrEmpty(taxForm.StrFilingModeIds))
                    {
                        var filingModeList = new List<FilingMode>();
                        foreach (var filingModeId in taxForm.StrFilingModeIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                        {
                            var filingMode = allFilingModes.Where(e => e.FilingModeId.ToString().Equals(filingModeId)).FirstOrDefault();
                            filingModeList.Add(filingMode);
                        }
                        taxForm.FilingModes = filingModeList.Select(e => new FilingMode()
                        {
                            Name = e.Name,
                            Description = e.Description
                        }).ToArray();
                    }

                    //payment modes
                    if (!string.IsNullOrEmpty(taxForm.StrPaymentModeIds))
                    {
                        var paymentModeList = new List<PaymentMode>();
                        foreach (var paymentModeId in taxForm.StrPaymentModeIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                        {
                            var paymentMode = allPaymentModes.Where(e => e.PaymentModeId.ToString().Equals(paymentModeId)).FirstOrDefault();
                            paymentModeList.Add(paymentMode);
                        }
                        taxForm.PaymentModes = paymentModeList.Select(e => new PaymentMode()
                        {
                            Name = e.Name,
                            Description = e.Description
                        }).ToArray();
                    }

                    //calendar fields
                    if (!string.IsNullOrEmpty(taxForm.StrCalenderFieldIds))
                    {
                        var calendarFieldList = new List<RequiredFilingCalendarDataField>();
                        foreach (var calendarFieldId in taxForm.StrCalenderFieldIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct())
                        {
                            var calendarField = allCalendarFields.Where(e => e.RequiredFilingCalendarDataFieldId.ToString().Equals(calendarFieldId)).FirstOrDefault();
                            calendarFieldList.Add(calendarField);
                        }
                        taxForm.RequiredFilingCalendarDataFields = calendarFieldList.Select(e => new RequiredFilingCalendarDataField()
                        {
                            Name = e.Name,
                            Description = e.Description
                        }).ToArray();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error: TaxFormCode is '{0}' and message = '{1}'", taxForm.LegacyReturnName, ex.Message));
                }
            }

            return list;
        }

        public List<PaymentInfoAdditionalFields> GetPaymentInfoRequiredFields(IEnumerable<string> scraperRegions)
        {
            using (PaymentInfoAdditionalFieldsDap dap = new PaymentInfoAdditionalFieldsDap(_connFactory.Connection))
            {
                var param = new DynamicParameters();
                string whereClause = string.Empty;

                if (scraperRegions != null && scraperRegions.Count() > 0)
                {
                    param.Add("@regions", scraperRegions.ToArray());
                    whereClause = "Where ScraperRegion in @regions";
                }
                return dap.Query<PaymentInfoAdditionalFields>(string.Format("select * from PaymentInfoAdditionalFields {0}", whereClause), param).ToList();
            }
        }

        #endregion
    }
}
