using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class ClientAPIKeysModel
    {
        public int Id { get; set; }
        public string APIKey { get; set; }
        public string AppName { get; set; }
        public int DepartmentId { get; set; }        
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
