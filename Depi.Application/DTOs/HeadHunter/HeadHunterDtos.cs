using DEPI.Domain.Entities.HeadHunters;

namespace DEPI.Application.DTOs.HeadHunter;

public class HeadHunterResponse
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public HeadHunterStatus Status { get; set; }
    public int TotalAssignmentsCompleted { get; set; }
    public int SuccessfulPlacements { get; set; }
    public decimal SuccessRate { get; set; }
    public decimal CommissionRate { get; set; }
}

public class AssignmentResponse
{
    public Guid Id { get; set; }
    public Guid HeadHunterId { get; set; }
    public string Requirements { get; set; } = string.Empty;
    public AssignmentStatus Status { get; set; }
    public int CandidatesSubmitted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}

public class TalentRecommendationResponse
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string AIAnalysis { get; set; } = string.Empty;
    public decimal MatchScore { get; set; }
    public decimal ProfileScore { get; set; }
    public RecommendationResult Result { get; set; }
    public DateTime CreatedAt { get; set; }
}