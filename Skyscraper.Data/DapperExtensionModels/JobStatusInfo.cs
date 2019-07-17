using Avalara.Skyscraper.Data.Dapper.Entities;

namespace Avalara.Skyscraper.Data.DapperExtensionModels
{
    public class JobStatusInfo : JobQueue
    {
        public string Mode { get; set; }
        public string HostIP { get; set; }
    }
}
