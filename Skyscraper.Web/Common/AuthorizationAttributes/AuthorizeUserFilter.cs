using System;
using System.Net;
using Avalara.Authentication;
using Avalara.Skyscraper.Models;
using Avalara.Skyscraper.Web.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Avalara.Skyscraper.Services;

namespace Avalara.Skyscraper.Web.Common
{
    public class AuthorizeUser : ActionFilterAttribute
    {
        protected UserEntity user;
        string[] arrauthorizeUsers = null;
        bool isUserAuthorized = false;
        AuthHelper auth = null;

        public AuthorizeUser(string authorizeUsers = null)
        {
            if (!string.IsNullOrEmpty(authorizeUsers))
                arrauthorizeUsers = authorizeUsers.Split(',');
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                auth = new AuthHelper(Startup.Configuration, context.HttpContext.RequestServices.GetService<ISkyscraperService>());

                user = context.HttpContext.GetAvaUser();
                auth.CreateUserAndRoleIfNotExist(context, user);
                var roleInfo = auth.GetRole(Convert.ToInt32(context.HttpContext.Items["DepartmentId"].ToString()), user.UserName);

                if (roleInfo != null)
                {
                    if (roleInfo.ExpirationDate < DateTime.Now)
                    {
                        context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "User account is expired.Please contact support.");
                        return;
                    }
                }
                if (arrauthorizeUsers != null && arrauthorizeUsers.Length > 0)
                {
                    foreach (var authorizeUser in arrauthorizeUsers)
                    {
                        Roles role;
                        if (Enum.TryParse(authorizeUser, out role))
                        {
                            if ((int)role == roleInfo.RoleId)
                            {
                                isUserAuthorized = true;
                            }
                        }
                    }

                    if (!isUserAuthorized)
                    {
                        context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "User not authorized for this operation");
                        return;
                    }
                }

                context.HttpContext.Items.Add("RoleId", roleInfo.RoleId);
            }
            catch (Exception ex)
            {
                context.GetCustomizedResponse(HttpStatusCode.InternalServerError, ex.Message);
                throw ex;
            }
        }
    }
}
