using System;
using System.Net;
using Avalara.Authentication;
using Avalara.Skyscraper.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Avalara.Skyscraper.Web.Common
{
    public class AuthorizeComplianceUser : ActionFilterAttribute
    {
        protected UserEntity user;
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                user = context.HttpContext.GetAvaUser();
                
                //Allow only if user is Skyscraper.SystemUser / Skyscraper.SystemUser.Compliance
                if (!user.IsSkyscraperSystemUser() && !user.IsComplianceUser())
                {
                    context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "User not authorized for this operation");
                    return;
                }
            }
            catch(Exception ex)
            {
                context.GetCustomizedResponse(HttpStatusCode.InternalServerError, ex.Message);
                throw ex;
            }
        }
    }
}
