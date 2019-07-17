using System;
using System.Collections.Generic;
using System.Text;

namespace Avalara.Skyscraper.Models
{
    public class SkyscraperUserModel
    {
        public int SkyscraperUserId { get; set; }
        public string AvaTaxUserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AvaTaxAccountId { get; set; }
        public int? AvaTaxUserRoleId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public string AISubjectId { get; set; }
    }
}
