using System;
using System.Net;
using Avalara.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Avalara.Skyscraper.Services;
using Microsoft.Extensions.DependencyInjection;
using Avalara.Skyscraper.Common.Extensions;

namespace Avalara.Skyscraper.Web.Common
{
    public class AuthorizeSkyscraperSystemUser : ActionFilterAttribute
    {
        protected UserEntity user;
        AuthHelper auth = null;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                auth = new AuthHelper(Startup.Configuration, context.HttpContext.RequestServices.GetService<ISkyscraperService>());

                user = context.HttpContext.GetAvaUser();
                var systemUsers = auth.GetSkyscraperSystemUser();

                //Allow only if user is Root/system user
                if (!systemUsers.Any(e => e.Trim().Equals(user.UserName, StringComparison.OrdinalIgnoreCase)))
                {
                    context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "User not authorized for this operation");
                    return;
                }
            }
            catch (Exception ex)
            {
                context.GetCustomizedResponse(HttpStatusCode.InternalServerError, ex.Message);
                throw ex;
            }
        }
    }
}
