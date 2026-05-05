using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

namespace DEPI.Domain.Entities.Coaching;

public class CoachingSession : AuditableEntity
{
    public Guid CoachId { get; set; }
    public Guid StudentId { get; set; }
    public string SessionType { get; set; } = "Weekly";
    public DateTime ScheduledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public SessionStatus Status { get; set; } = SessionStatus.Scheduled;
    public string Agenda { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string Feedback { get; set; } = string.Empty;
    public string ActionItems { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsReschedule { get; set; }

    public virtual User Coach { get; set; } = null!;
    public virtual User Student { get; set; } = null!;

    public void Start() { Status = SessionStatus.InProgress; StartedAt = DateTime.UtcNow; }
    public void Complete(string notes, string feedback, string actionItems, int rating)
    {
        Status = SessionStatus.Completed; CompletedAt = DateTime.UtcNow;
        Notes = notes; Feedback = feedback; ActionItems = actionItems; Rating = rating;
    }
    public void Cancel(string reason) { Status = SessionStatus.Cancelled; Notes = reason; }
}

public class CoachProfile : AuditableEntity
{
    public Guid UserId { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public int TotalSessions { get; set; }
    public int ActiveStudents { get; set; }
    public decimal AverageRating { get; set; }
    public decimal HourlyRate { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string Certifications { get; set; } = string.Empty;
    public int MaxStudents { get; set; } = 10;

    public virtual User User { get; set; } = null!;

    public void UpdateRating(int newRating)
    {
        AverageRating = TotalSessions > 0
            ? ((AverageRating * (TotalSessions - 1) + newRating) / TotalSessions)
            : newRating;
    }
}

public enum SessionStatus { Scheduled, InProgress, Completed, Cancelled, NoShow }
