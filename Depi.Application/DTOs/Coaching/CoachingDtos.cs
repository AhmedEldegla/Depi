using DEPI.Domain.Entities.Coaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI.Application.DTOs.Coaching
{
    public class CoachingSessionResponse
    {
        public Guid Id { get; set; }
        public string SessionType { get; set; } = string.Empty;
        public DateTime ScheduledAt { get; set; }
        public SessionStatus Status { get; set; }
        public string Agenda { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Feedback { get; set; } = string.Empty;
        public string ActionItems { get; set; } = string.Empty;
        public int Rating { get; set; }
    }

    public class CoachProfileResponse
    {
        public Guid Id { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public int TotalSessions { get; set; }
        public int ActiveStudents { get; set; }
        public decimal AverageRating { get; set; }
        public decimal HourlyRate { get; set; }
        public string Certifications { get; set; } = string.Empty;
    }

    public record ScheduleSessionRequest(
        Guid CoachId,
        DateTime ScheduledAt,
        string SessionType,
        string Agenda
    );

    public record CompleteSessionRequest(
        Guid SessionId,
        string Notes,
        string Feedback,
        string ActionItems,
        int Rating
    );

    public record RegisterCoachRequest(
        string Specialization,
        string Bio,
        int YearsOfExperience,
        decimal HourlyRate,
        string? Certifications
    );
}
