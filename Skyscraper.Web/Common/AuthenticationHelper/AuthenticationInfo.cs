using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Common
{
    class AuthenticationInfo :IAuthenticationInfo
    {
        public int? AvaTaxUserId { get; set; }
        public int? AvaTaxAccountId { get; set; }
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string BearerToken { get; set; }
    }
}
