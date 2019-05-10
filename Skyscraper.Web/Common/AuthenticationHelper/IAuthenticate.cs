using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Common
{
    public interface IAuthenticate
    {
        IAuthenticationInfo ValidateAccessKey(string accessKey);
        IAuthenticationInfo ValidateAccessKeyUsingRestV2(string accessKey);
        IAuthenticationInfo GetBasicAuthenticatedUser(string authHeader);
        void SetAuthenticatedUserInContext(IAuthenticationInfo userInfo, HttpContext context);

    }
}
