using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;

namespace DEPI.Domain.Modules.Companies.Entities
{
    public class CompanyMember : AuditableEntity
    {
        public string MemberRole {  get; set; } = string.Empty;
        public DateTime JoinedAt {  get; set; } = DateTime.Now;

        // Forign keys and Navigation Properties
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }



    }
}
