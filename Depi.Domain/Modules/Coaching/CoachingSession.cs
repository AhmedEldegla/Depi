namespace DEPI.Domain.Entities.Coaching;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class CoachingSession : AuditableEntity
{
    public Guid CoachId { get;set; }
    public Guid ClientId { get;set; }
    public Guid? ProjectId { get;set; }
    public Guid? ContractId { get;set; }
    public string Type { get;set; } = "general";
    public CoachingStatus Status { get;set; }
    public string? Subject { get;set; }
    public string? Notes { get;set; }
    public DateTime ScheduledAt { get;set; }
    public int DurationMinutes { get;set; }
    public DateTime? CompletedAt { get;set; }
    public int Rating { get;set; }
    public string? Feedback { get;set; }

    public User? Coach { get;set; }
    public User? Client { get;set; }
    public Projects.Project? Project { get;set; }
    public Projects.Contract? Contract { get;set; }
    public ICollection<CoachingMilestone> Milestones { get;set; } = new HashSet<CoachingMilestone>();

    private CoachingSession() { }

    public static CoachingSession Create(
        Guid coachId,
        Guid clientId,
        string type,
        string? subject,
        DateTime scheduledAt,
        int durationMinutes)
    {
        return new CoachingSession
        {
            CoachId = coachId,
            ClientId = clientId,
            Type = type,
            Subject = subject,
            Status = CoachingStatus.Scheduled,
            ScheduledAt = scheduledAt,
            DurationMinutes = durationMinutes
        };
    }

    public void LinkToProject(Guid projectId)
    {
        ProjectId = projectId;
    }

    public void LinkToContract(Guid contractId)
    {
        ContractId = contractId;
    }

    public void Start()
    {
        Status = CoachingStatus.InProgress;
    }

    public void Complete(string? notes = null)
    {
        Status = CoachingStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(notes))
            Notes = notes;
    }

    public void Cancel(string reason)
    {
        Status = CoachingStatus.Cancelled;
    }

    public void AddRating(int rating, string? feedback)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5");
        
        Rating = rating;
        Feedback = feedback;
    }
}

public class CoachingMilestone : BaseEntity
{
    public Guid CoachingSessionId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public CoachingMilestoneStatus Status { get;set; }
    public DateTime? DueDate { get;set; }
    public DateTime? CompletedAt { get;set; }
    public string? Notes { get;set; }

    public virtual CoachingSession? Session { get;set; }

    private CoachingMilestone() { }

    public static CoachingMilestone Create(
        Guid sessionId,
        string title,
        string description,
        DateTime? dueDate = null)
    {
        return new CoachingMilestone
        {
            CoachingSessionId = sessionId,
            Title = title,
            Description = description,
            DueDate = dueDate,
            Status = CoachingMilestoneStatus.Pending
        };
    }

    public void Complete(string? notes = null)
    {
        Status = CoachingMilestoneStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        if (!string.IsNullOrEmpty(notes))
            Notes = notes;
    }

    public void MarkAsInProgress()
    {
        Status = CoachingMilestoneStatus.InProgress;
    }
}

public enum CoachingStatus
{
    Scheduled = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}

public enum CoachingMilestoneStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3,
    Blocked = 4
}
