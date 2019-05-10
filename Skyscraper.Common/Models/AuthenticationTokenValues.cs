using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Common
{
    public class AuthenticationTokenValues
    {
        public String Username { get; set; }
        public String DateIssuedString { get; set; }
        public DateTime DateIssued { get; set; }
        public String Signature { get; set; }
    }
}
