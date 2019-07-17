using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class WebFileDataModel
    {
        public Int64 JobId { get; set; }
        public String FilingData { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
