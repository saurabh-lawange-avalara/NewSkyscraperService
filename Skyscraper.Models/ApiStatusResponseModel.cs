using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class ApiStatusResponseModel
    {
        public Int32 Id { get; set; }

        public String ApiName { get; set; }

        public String Method { get; set; }

        public Boolean Status { get; set; }

        public Int32 ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
