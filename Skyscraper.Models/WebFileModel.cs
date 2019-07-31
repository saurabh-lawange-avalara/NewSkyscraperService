using Avalara.Returns.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class WebFileModel
    {

        [Required]
        public ComputedFormData CFD { get; set; }
        public List<SkyscraperUploadFiles> UploadFiles { get; set; }
        [Required]
        public OrderedDictionary FilingCalendarData { get; set; }
        public string SkyScraperRegion { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Mode { get; set; }
        public long RefJobId { get; set; }
        [Required]
        public int DueDay { get; set; }
        public PaymentInfoRequiredFields Payment { get; set; }
        public string BulkAccount { get; set; }
        public long BulkRequestId { get; set; }
        public string S3CFDRefKey { get; set; }
    }

}
