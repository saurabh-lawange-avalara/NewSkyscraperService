using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalara.Skyscraper.Common
{
    public class AbsoluteStringComparer : IEqualityComparer<string>
    {

        public bool Equals(string x, string y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.Trim().ToUpper() == y.Trim().ToUpper();
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }

}
