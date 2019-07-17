using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class SkyScraperErrorModel
    {
        /// <summary>
        /// Type of the error on DOR.Can be website changes related or Calendar data validations
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// Error message occured while processing the request
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Step where error occured.Can be before submitting the return or post submit like making payment or downloading confirmation.
        /// </summary>
        public string ErrorStep { get; set; }
    }
}
