using Depi.Domain.Modules.Learning.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Learning.Entities
{
    public class Course : BaseEntity
    {
        public Guid InstructorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsFree { get; set; }
        public int DurationInHours { get; set; }
        public int EnrollmentCount { get; set; }
        public double AverageRating { get; set; }
        public CourseLevel Level { get; set; }
        public CourseStatus Status { get; set; }

        // Navigation Properties
        public User Instructor { get; set; } = null!;
        public ICollection<CourseEnrollment> Enrollments { get; set; } = [];
    }
}
