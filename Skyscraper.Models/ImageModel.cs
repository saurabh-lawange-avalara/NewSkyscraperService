using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class ImageModel
    {
        public long ImageId { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string Tags { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
