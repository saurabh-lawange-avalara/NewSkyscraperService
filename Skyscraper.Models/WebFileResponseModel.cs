using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class WebFileResponseModel
    {
        /// <summary>
        /// Request Job Id
        /// </summary>
        public long JobId { get; set; }
        /// <summary>
        /// Job Status of the Job Id
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Response message which signifies any error or warning from DOR
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Webfile Job Mode which can be Review/File/FileComplete/Confirmation
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// Job processed date and time by scraper
        /// </summary>
        public DateTime ProcessedOnDateTime { get; set; }

        /// <summary>
        /// All efile and epay confirmations from DOR for the filing period
        /// </summary>
        public ImageModel[] Confirmations { get; set; }

        /// <summary>
        /// Filing steps images 
        /// </summary>
        public ImageModel[] Images { get; set; }

        /// <summary>
        /// All filing related data like TaxAmount,Registration ID,frequency and confirmation ids
        /// </summary>
        public Dictionary<string, object> FilingData { get; set; }
        /// <summary>
        /// If there is any error while processing the request.
        /// </summary>
        public SkyScraperErrorModel Error { get; set; }

        /// <summary>
        /// Account type which is used to login into DOR for the particular request i.e IndividualAccount or BulkAccount
        /// Null signifies history Job(before january filing period 2019) or login type could not be resolved
        /// </summary>
        public string LoginAccountType { get; set; }
    }
}
