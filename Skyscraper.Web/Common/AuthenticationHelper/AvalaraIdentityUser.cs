using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Web.Common
{
    public class AvalaraIdentityUser
    {
        public string avatax_user_id { get; set; }

        public string avatax_account_id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string family_name { get; set; }

        public string given_name { get; set; }
    }
}
