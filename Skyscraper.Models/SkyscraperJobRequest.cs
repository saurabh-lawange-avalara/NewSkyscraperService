using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Avalara.Skyscraper.Models
{
    public class SkyscraperJobRequest : SkyscraperJob
    {
        public int CompanyId { get; set; }
        public int AccountId { get; set; }
        public long AvaTaxUserId { get; set; }
    }

    public class SkyscraperJob 
    {
        public long JobId { get; set; }
        public JobType JobType { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AdditionalOptions { get; set; }
        public long BulkRequestId { get; set; }
        public byte Priority { get; set; }
        public string ReturnsDataJson { get; set; }
        public string ReturnsDataFileKey { get; set; }
        public string ExpectedFilingDataJson { get; set; }
        public short FilingYear { get; set; }
        public byte FilingMonth { get; set; }
        public FilingFrequency FilingFrequencyId { get; set; }
        public int FilingDueDay { get; set; }
        public long RefJobId { get; set; }
        public List<SkyscraperUploadFiles> Files { get; set; }
        public string TaxFormCode { get; set; }
        public string LegacyReturnName { get; set; }
        public string Mode { get; set; }
        public string CustomerIdentifier { get; set; }
        public string ScriptChecksum { get; set; }
        public dynamic PaymentDetails { get; set; }
        public string BulkAccountUserId { get; set; }
        public int? ClientApiKeyId { get; set; }
        public int? DepartmentId { get; set; }
        public int JobStatusId { get; set; }
    }

}
