using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Common
{
    public interface IAuthenticationInfo:IIdentity
    {
        int? AvaTaxUserId { get; set; }
        int? AvaTaxAccountId { get; set; }
        string BearerToken { get; set; }
    }
}
