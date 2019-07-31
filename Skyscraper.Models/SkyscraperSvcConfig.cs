using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Avalara.Skyscraper.Model
{
    public class SkyscraperSvcConfig
    {
        public OptionalFields OptionalFields { get; set; }
        public string UpdateFileNameInRequest { get; set; }
        public string StatesWithSeparatePaymentSites { get; set; }
        public Dictionary<string, long> TestJobs { get; set; }
        public AllowedModesOnTest AllowedModesOnTest { get; set; }
    }

    public class OptionalFields
    {
        public string OptionalPaymentFields { get; set; }
    }

    public class AllowedModesOnTest
    {
        public bool IsFileModeDisabledInTest { get; set; }
        public string SupportedModesInTest { get; set; }
    }
}
