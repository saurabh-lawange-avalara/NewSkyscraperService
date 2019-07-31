using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class PaymentFields 
    {
        public string ScraperRegion { get; set; }
        public string Country { get; set; }

        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public bool ShowValue { get; set; }
        public string MaskValue { get; set; }
        public string RegEx { get; set; }
    }
}
