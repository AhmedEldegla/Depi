using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Domain.Modules.Coaching.Entities
{
    public class CoachingSession : BaseEntity
    {
        public Guid CoachId { get; set; }
        public Guid StudentId { get; set; }
        public string Topic { get; set; }
        public string? Description { get; set; }
        public SessionType Type { get; set; }
        public SessionStatus Status { get; set; }
        public DateTime ScheduledAt { get; set; }
        public int DurationInMinutes { get; set; }
        public decimal Price { get; set; }
        public decimal? Rating { get; set; }
        public string? Feedback { get; set; }
        public User Coach { get; set; } = null!;
        public User Student { get; set; } = null!;


    }
}
