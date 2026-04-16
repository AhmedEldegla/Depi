using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Learning.Entities
{
    public class CourseEnrollment : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }      // nullable 
        public decimal ProgressPercentage { get; set; } // 0 - 100

        // Navigation Properties
        public Course Course { get; set; } = null!;
        public User Student { get; set; } = null!;
    }
}
