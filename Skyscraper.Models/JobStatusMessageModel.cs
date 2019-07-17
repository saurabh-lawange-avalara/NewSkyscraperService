using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class JobStatusMessageModel
    {
        public long JobId { get; set; }
        public JobStatus Status { get; set; }
        public JobType JobType { get; set; }
        public string Message { get; set; }
        public string InternalMessage { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime QueueDateTime { get; set; }
        public DateTime JobStartDateTime { get; set; }
        public SkyScraperErrorModel Error { get; set; }
        public string Mode { get; set; }
        public string HostIP { get; set; }
    }
}
