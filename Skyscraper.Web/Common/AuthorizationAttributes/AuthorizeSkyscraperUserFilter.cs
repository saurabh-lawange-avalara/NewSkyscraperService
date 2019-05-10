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
    public class AuthorizeSkyscraperUser : ActionFilterAttribute
    {
        protected UserEntity user;
        string[] arrauthorizeUsers = null;
        bool isUserAuthorized = false;
        AuthHelper auth = null;

        public AuthorizeSkyscraperUser(string authorizeUsers = null)
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

                if (!auth.IsSkyscraperUser(Convert.ToInt32(context.HttpContext.Items["DepartmentId"].ToString())))
                {
                    context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "You don't have enough persmission to view or update this information.");
                    return;
                }

                var roleInfo = auth.GetRole(Convert.ToInt32(context.HttpContext.Items["DepartmentId"].ToString()), user.UserName);

                if (roleInfo != null)
                {
                    if (roleInfo.ExpirationDate < DateTime.Now)
                    {
                        context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "User account is expired.Please contact support.");
                        return;
                    }
                }
                else
                {
                    context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "You don't have enough persmission to view or update this information.");
                    return;
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
                        context.GetCustomizedResponse(HttpStatusCode.Unauthorized, "You don't have enough persmission to view or update this information.");
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
