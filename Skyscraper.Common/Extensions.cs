using System;
using System.Linq;
using System.Net;
using Avalara.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Avalara.Skyscraper.Common.Extensions
{
    public static class Extensions
    {
        private const string AvaUserKey = "AvaUser";


        public static UserEntity GetAvaUser(this HttpContext context)
        {
            return context.Items[AvaUserKey] as UserEntity;
        }

        public static void SetAvaUser(this HttpContext context, UserEntity user)
        {
            context.Items[AvaUserKey]  = user;
        }

        public static bool IsSkyscraperSystemUser(this UserEntity user)
        {
            if (user.UserName.ToUpper() == "SKYSCRAPER.SYSTEMUSER.COMPLIANCE" || user.UserName.ToUpper() == "SKYSCRAPER.SYSTEMUSER")
            {
                return true;
            }
            return false;
        }

        public static bool IsComplianceUser(this UserEntity user)
        {
            if (user.SecurityRoleId == 14 || user.SecurityRoleId == 15 || user.SecurityRoleId == 19 || user.SecurityRoleId == 20)
            {
                return true;
            }
            return false;
        }

        //public static String GetHeader(this HttpRequest request, String key)
        //{
        //    var headerValue = (HttpContext.Current.Request.Headers.GetValues(key) ?? new String[0]).FirstOrDefault();
        //    return headerValue;
        //}

        //public static String GetCookie(this HttpRequest request, String key)
        //{
        //    var cookie = HttpContext.Current.Request.Cookies[key];
        //    return cookie != null ? cookie.Value : null;
        //}

        public static ActionExecutingContext GetCustomizedResponse(this ActionExecutingContext httpActionContext, HttpStatusCode httpstatusCode, string message)
        {
            httpActionContext.HttpContext.Response.StatusCode = (int)httpstatusCode; //Unauthorized
            httpActionContext.HttpContext.Response.Headers.Clear();
            var wrongResult = new { message = message };
            httpActionContext.Result = new JsonResult(wrongResult);
            return httpActionContext;
        }

        public static bool CompareValues(this string value, string contents)
        {
            //compare multiple strings present in the parent string.send comma(,) seperated strings in contents.
            bool isSuccess = false;
            foreach (var content in contents.Split(','))
            {
                if (value.IndexOf(content, StringComparison.OrdinalIgnoreCase) >= 0)
                    isSuccess = true;
                else
                    return false;

            }
            return isSuccess;
        }

    }
}
