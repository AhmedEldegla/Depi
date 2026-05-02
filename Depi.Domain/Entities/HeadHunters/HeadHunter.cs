using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Recruitment;

namespace DEPI.Domain.Entities.HeadHunters;

public class HeadHunter : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public HeadHunterStatus Status { get; set; } = HeadHunterStatus.Active;
    public int TotalAssignmentsCompleted { get; set; }
    public int TotalCandidatesSubmitted { get; set; }
    public int SuccessfulPlacements { get; set; }
    public decimal SuccessRate { get; set; }
    public decimal AverageResponseTimeHours { get; set; }
    public decimal CommissionRate { get; set; } = 0.05m;
    public DateTime? LastActiveAt { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual ICollection<HeadHunterAssignment> Assignments { get; set; } = new List<HeadHunterAssignment>();

    public void Activate()
    {
        if (Status == HeadHunterStatus.Active)
            throw new InvalidOperationException("Head hunter is already active");
        Status = HeadHunterStatus.Active;
        LastActiveAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (Status != HeadHunterStatus.Active)
            throw new InvalidOperationException("Head hunter is not active");
        Status = HeadHunterStatus.Inactive;
    }

    public void RecordSuccess()
    {
        SuccessfulPlacements++;
        UpdateSuccessRate();
    }

    public void IncrementAssignments()
    {
        TotalAssignmentsCompleted++;
        LastActiveAt = DateTime.UtcNow;
    }

    public void IncrementSubmissions()
    {
        TotalCandidatesSubmitted++;
        LastActiveAt = DateTime.UtcNow;
    }

    private void UpdateSuccessRate()
    {
        SuccessRate = TotalAssignmentsCompleted > 0
            ? (decimal)SuccessfulPlacements / TotalAssignmentsCompleted
            : 0;
    }
}

public class HeadHunterAssignment : AuditableEntity
{
    public Guid HeadHunterId { get; set; }
    public Guid? ClientId { get; set; }
    public string? ProjectId { get; set; }
    public string? JobId { get; set; }
    public string Requirements { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public AssignmentStatus Status { get; set; } = AssignmentStatus.Active;
    public int CandidatesSubmitted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string? Feedback { get; set; }

    public virtual HeadHunter HeadHunter { get; set; } = null!;
    public virtual User? Client { get; set; }

    public void Complete(string? feedback)
    {
        Status = AssignmentStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        Feedback = feedback;
    }

    public void Cancel(string reason)
    {
        Status = AssignmentStatus.Cancelled;
        Feedback = reason;
    }

    public void IncrementCandidates()
    {
        CandidatesSubmitted++;
    }
}

public class TalentRecommendation : AuditableEntity
{
    public Guid AssignmentId { get; set; }
    public string RecommendedUserId { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string AIAnalysis { get; set; } = string.Empty;
    public decimal MatchScore { get; set; }
    public decimal ProfileScore { get; set; }
    public decimal SkillsScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public RecommendationResult Result { get; set; } = RecommendationResult.Pending;
    public string? ClientFeedback { get; set; }
    public DateTime? ReviewedAt { get; set; }

    public virtual HeadHunterAssignment Assignment { get; set; } = null!;
    public virtual User RecommendedUser { get; set; } = null!;

    public void Accept()
    {
        Result = RecommendationResult.Accepted;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Reject(string? feedback)
    {
        Result = RecommendationResult.Rejected;
        ClientFeedback = feedback;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Hire()
    {
        Result = RecommendationResult.Hired;
        ReviewedAt = DateTime.UtcNow;
    }
}

public enum HeadHunterStatus { Active, Inactive, Suspended }
public enum AssignmentStatus { Active, Completed, Cancelled, Expired }
public enum RecommendationResult { Pending, Accepted, Rejected, Hired }
