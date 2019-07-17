using System;
using System.Net;
using Avalara.Authentication;
using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Services;
using Avalara.Skyscraper.Web.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Avalara.Skyscraper.Web.Common
{
    public class AuthorizeJobOwnerAccount : ActionFilterAttribute
    {
        protected UserEntity user;
        AuthHelper auth = null;
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                auth = new AuthHelper(Startup.Configuration, context.HttpContext.RequestServices.GetService<ISkyscraperService>());

                user = context.HttpContext.GetAvaUser();

                if (user == null)
                {
                    context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "User not valid");
                    return;
                }
                object deptId;
                if (!context.HttpContext.Items.TryGetValue("DepartmentId", out deptId))
                {
                    deptId = 0;
                }

                //allow skyscraper user 
                if (user.IsSkyscraperSystemUser() 
                    || user.IsComplianceUser() || auth.IsSkyscraperUser((int)deptId))
                {
                    return;
                }

                object jobId, jobIds;
                
                if (context.ActionArguments.TryGetValue("id", out jobId))
                {
                    //account info from db
                    var jobOwnerInfo = auth.GetAccountInfoFromJobData((long)jobId);

                    //send it for validating job owner
                    if (!IsUserJobOwner(context, jobOwnerInfo, false))
                    {
                        return;
                    }
                }

                //If the GET is for list of JobIds
                if (context.ActionArguments.TryGetValue("jobIds", out jobIds))
                {
                    var jobIdList = ((string)jobIds).Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var oneJobId in jobIdList)
                    {
                        //get account info for each job and send it for validating job owner
                        var jobOwnerInfo = auth.GetAccountInfoFromJobData((long)jobId);

                        if (!IsUserJobOwner(context, jobOwnerInfo, true))
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                context.GetCustomizedResponse(HttpStatusCode.InternalServerError, ex.Message);
                return;
            }
        }

        public bool IsUserJobOwner(ActionExecutingContext context, JobOwnerAccountInfoModel jobOwnerInfo, bool isJobIdList = false)
        {
            //check if account id and client id are matching to validate job owner account
            string error = string.Empty;
            if (jobOwnerInfo == null)
            {
                error = isJobIdList ? "One or more jobs are not present." : "Job Id not present.";
                context.GetCustomizedResponse(HttpStatusCode.Unauthorized, error);
                return false;
            }
            //account id is set to 0 for all configured jobs for success, failure and exception
            //if account id is 0 it means all clients can access those job response.
            if (jobOwnerInfo.AccountId > 0 && user.AccountId != jobOwnerInfo.AccountId)
            {
                error = isJobIdList ? "User not authorized for one or more jobs from the list." : "User not authorized for this job";
                context.GetCustomizedResponse(HttpStatusCode.Unauthorized, error);
                return false;
            }

            //if clientapikey id not present means its a history job.bypass this check
            if (jobOwnerInfo.ClientApiKeyId > 1)
            {
                object _apiKeyId = 0;
                if (!context.HttpContext.Items.TryGetValue("ClientApiKeyId", out _apiKeyId))
                {
                    _apiKeyId = 0;
                }
                if (jobOwnerInfo.ClientApiKeyId != (int)_apiKeyId)
                {
                    error = isJobIdList ? "User not authorized for these Jobs." : "User not authorized for this Job";
                    context.GetCustomizedResponse(HttpStatusCode.Unauthorized, error);
                    return false;
                }
            }
            return true;
        }
    }
}
