using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class SkyscraperUploadFiles
    {
        public Int64? JobId { get; set; }         // Make nullable to clean Uploadfile object in swagger (WEBAUTO-5429)
        /// <summary>
        /// Name of the file
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// byte array 
        /// </summary>
        public Byte[] Content { get; set; }
        /// <summary>
        /// s3 reference key. send the s3 location of file if content is null
        /// </summary>
        public String FileKey { get; set; }
    }
}
