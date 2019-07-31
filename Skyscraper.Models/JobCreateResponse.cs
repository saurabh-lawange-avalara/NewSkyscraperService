using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class JobCreateResponse 
    {
        public long JobId { get; set; }
        public OperationStatus Status { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
