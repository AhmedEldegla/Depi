using DEPI.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Recruitment.Entities
{
    public class JobPost : BaseEntity
    {
        public Guid HeadHunterId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public JobType Type { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public string? Location { get; set; }
        public JobPostStatus Status { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation Properties
        public HeadHunter HeadHunter { get; set; } = null!;
        public ICollection<JobApplication> Applications { get; set; } = [];
    }
}
