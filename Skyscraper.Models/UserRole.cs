using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class ServiceModel_UserRole
    {
        public Int32 Id { get; set; }
        public Int32? RoleId { get; set; }
        public Int64? SkyscraperUserId { get; set; }
        public Int32? DepartmentId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UserName { get; set; }
    }
}
