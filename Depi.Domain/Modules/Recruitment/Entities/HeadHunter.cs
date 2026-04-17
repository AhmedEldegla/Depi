using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Recruitment.Entities
{
    public class HeadHunter : BaseEntity
    {
        public Guid UserId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public int SuccessfulPlacements { get; set; }
        public decimal CommissionRate { get; set; }
        public decimal TotalEarnings { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public ICollection<JobPost> JobPosts { get; set; } = [];
    }
}
