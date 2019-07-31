using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Avalara.Skyscraper.Models
{

    /// <summary>
    /// Classes used for V2 API    
    /// </summary>
    public class FormStatus : FormStatusResponse
    {
        public string StrBulkAccounts { get; set; }
        public bool ScraperStatus { get; set; }
        public string StrFilingModeIds { get; set; }
        public string StrPaymentModeIds { get; set; }
        public string StrCalenderFieldIds { get; set; }
        public Int32? WebfilingAccount { get; set; }
    }

}
