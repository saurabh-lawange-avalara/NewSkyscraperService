using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class SkyScraperResourceModel
    {
        public Int64 SSResourceId { get; set; }

        public String Name { get; set; }

        public String Comments { get; set; }

        public Int64 JobId { get; set; }

        public String FileKey { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public Int32? FileType { get; set; }

        public string Tags { get; set; }

        public byte[] ImageData { get; set; }
    }
}
