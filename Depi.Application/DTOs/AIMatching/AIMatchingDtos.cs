using DEPI.Domain.Entities.AIMatching;

namespace DEPI.Application.DTOs.AIMatching;

public class FreelancerScoreResponse
{
    public Guid Id { get; set; }
    public string FreelancerName { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ProjectSuccessScore { get; set; }
    public decimal CommunicationScore { get; set; }
    public decimal ReliabilityScore { get; set; }
    public decimal QualityScore { get; set; }
    public decimal ClientSatisfactionScore { get; set; }
    public decimal CompletionRateScore { get; set; }
    public int TotalProjects { get; set; }
    public int CompletedProjects { get; set; }
    public decimal AverageRating { get; set; }
    public DateTime CalculatedAt { get; set; }
}

public class ProjectMatchResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; } = string.Empty;
    public string FreelancerName { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal BudgetScore { get; set; }
    public string AIReasoning { get; set; } = string.Empty;
    public MatchStatus Status { get; set; }
    public int Rank { get; set; }
    public DateTime CalculatedAt { get; set; }
}

public class RecommendationResponse
{
    public Guid Id { get; set; }
    public RecommendationType Type { get; set; }
    public string TargetType { get; set; } = string.Empty;
    public Guid TargetId { get; set; }
    public decimal ConfidenceScore { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}