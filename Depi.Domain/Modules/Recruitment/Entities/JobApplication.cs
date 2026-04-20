using Depi.Domain.Modules.Recruitment.Enum;
using DEPI.Domain.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Recruitment.Entities
{
    public class JobApplication : BaseEntity
    {
        public Guid JobPostId { get; set; }
        public Guid FreelancerId { get; set; }
        public string? CoverLetter { get; set; }
        public string? ResumeUrl { get; set; }
        public JobApplicationStatus Status { get; set; }

        // Navigation Properties
        public JobPost JobPost { get; set; } = null!;
        //public Freelancer Freelancer { get; set; } = null!;
    }
}
