using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    /// <summary>
    /// Response Class for Form Status V2 API
    /// <param name="IsWebfileForm">Indicates form has webfile support in scraper</param>
    /// <param name="IsAvailable">Indicates if form is temporarily disabled</param>
    /// </summary>
    public class FormStatusResponse 
    {
        public int TaxFormId { get; set; }
        public string TaxFormCode { get; set; }
        public string LegacyReturnName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string ScraperRegion { get; set; }

        [Description("Indicates form has webfile support in scraper.")]
        public bool IsWebfileForm { get; set; }

        [Display(Name = "Available")]
        [Description("Indicates if form is temporarily disabled.")]
        public bool IsAvailable { get; set; }

        [Display(Name = "File Upload")]
        public bool FileUpload { get; set; }
        public string[] BulkAccounts { get; set; }
        public PropertyDiscriptor[] FilingModes { get; set; }
        public PropertyDiscriptor[] PaymentModes { get; set; }
        public PropertyDiscriptor[] RequiredFilingCalendarDataFields { get; set; }

        [Display(Name = "Bulk Support")]
        public bool IsBulkSupported { get; set; }

        [Display(Name = "Individual Support")]
        public bool IsIndividualSupported { get; set; }

        [Display(Name = "Default TaxForm")]
        public bool? IsDefaultTaxForm { get; set; }

        [Display(Name = "Filing Disabled Reason")]
        public string FilingDisabledReason { get; set; }

    }
}
