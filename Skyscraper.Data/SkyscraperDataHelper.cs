using Avalara.Skyscraper.Data.Dapper.Entities;
using Avalara.Skyscraper.Data.DapperExtensionModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
