using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Recruitment;
using DEPI.Domain.Entities.Profiles;

namespace DEPI.Domain.Entities.AIMatching;

public class SkillMatch : AuditableEntity
{
    public Guid ProjectId { get; set; }
    public Guid FreelancerId { get; set; }
    public Guid SkillId { get; set; }
    public int RequiredLevel { get; set; }
    public int FreelancerLevel { get; set; }
    public decimal MatchScore { get; set; }
    public bool IsMatch { get; set; }
    public string Analysis { get; set; } = string.Empty;
    public virtual Project Project { get; set; } = null!;
    public virtual User Freelancer { get; set; } = null!;
    public virtual Skill Skill { get; set; } = null!;
}

public class ProjectMatch : AuditableEntity
{
    public Guid ProjectId { get; set; }
    public Guid FreelancerId { get; set; }
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal BudgetScore { get; set; }
    public decimal AvailabilityScore { get; set; }
    public decimal ReputationScore { get; set; }
    public decimal LocationScore { get; set; }
    public string AIReasoning { get; set; } = string.Empty;
    public MatchStatus Status { get; set; } = MatchStatus.Pending;
    public bool IsShortlisted { get; set; }
    public int Rank { get; set; }
    public string Recommendation { get; set; } = string.Empty;
    public DateTime CalculatedAt { get; set; }
    public virtual Project Project { get; set; } = null!;
    public virtual User Freelancer { get; set; } = null!;
}

public class JobMatch : AuditableEntity
{
    public Guid JobId { get; set; }
    public Guid FreelancerId { get; set; }
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal SalaryScore { get; set; }
    public decimal LocationScore { get; set; }
    public string AIReasoning { get; set; } = string.Empty;
    public MatchStatus Status { get; set; } = MatchStatus.Pending;
    public bool IsShortlisted { get; set; }
    public int Rank { get; set; }
    public string Recommendation { get; set; } = string.Empty;
    public DateTime CalculatedAt { get; set; }
    public virtual Job Job { get; set; } = null!;
    public virtual User Freelancer { get; set; } = null!;
}

public class FreelancerScore : AuditableEntity
{
    public Guid FreelancerId { get; set; }
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ProjectSuccessScore { get; set; }
    public decimal CommunicationScore { get; set; }
    public decimal ReliabilityScore { get; set; }
    public decimal ResponsivenessScore { get; set; }
    public decimal QualityScore { get; set; }
    public decimal EarningsScore { get; set; }
    public decimal ClientSatisfactionScore { get; set; }
    public decimal CompletionRateScore { get; set; }
    public int TotalProjects { get; set; }
    public int CompletedProjects { get; set; }
    public decimal AverageRating { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal ResponseRate { get; set; }
    public decimal OnTimeDeliveryRate { get; set; }
    public DateTime CalculatedAt { get; set; }

    public virtual User Freelancer { get; set; } = null!;

    public void UpdateScore(decimal overallScore, decimal skillScore, decimal projectSuccessScore,
        decimal communicationScore, decimal reliabilityScore, decimal responsivenessScore,
        decimal qualityScore, decimal earningsScore, decimal clientSatisfactionScore,
        decimal completionRateScore)
    {
        OverallScore = overallScore;
        SkillScore = skillScore;
        ProjectSuccessScore = projectSuccessScore;
        CommunicationScore = communicationScore;
        ReliabilityScore = reliabilityScore;
        ResponsivenessScore = responsivenessScore;
        QualityScore = qualityScore;
        EarningsScore = earningsScore;
        ClientSatisfactionScore = clientSatisfactionScore;
        CompletionRateScore = completionRateScore;
        CalculatedAt = DateTime.UtcNow;
    }
}

public class ScoringRule : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ScoringRuleType Type { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public decimal Weight { get; set; } = 1.0m;
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public decimal DefaultScore { get; set; }
    public string Formula { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    public virtual ICollection<ScoringCriteria> Criterias { get; set; } = new List<ScoringCriteria>();
}

public class ScoringCriteria : AuditableEntity
{
    public Guid RuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public decimal Weight { get; set; } = 1.0m;
    public bool IsActive { get; set; } = true;

    public virtual ScoringRule Rule { get; set; } = null!;
}

public class Recommendation : AuditableEntity
{
    public Guid UserId { get; set; }
    public RecommendationType Type { get; set; }
    public string TargetType { get; set; } = string.Empty;
    public Guid TargetId { get; set; }
    public decimal ConfidenceScore { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsViewed { get; set; }
    public bool IsClicked { get; set; }
    public DateTime? ClickedAt { get; set; }

    public virtual User User { get; set; } = null!;
}

public class AILog : AuditableEntity
{
    public Guid UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;
    public string Output { get; set; } = string.Empty;
    public decimal ConfidenceScore { get; set; }
    public string Model { get; set; } = string.Empty;
    public long ProcessingTimeMs { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string Metadata { get; set; } = string.Empty;

    public virtual User User { get; set; } = null!;
}

public enum MatchStatus
{
    Pending,
    Calculated,
    Shortlisted,
    Applied,
    Rejected,
    Hired
}

public enum ScoringRuleType
{
    SkillMatch,
    ExperienceMatch,
    BudgetMatch,
    LocationMatch,
    Reputation,
    Availability,
    Communication,
    Reliability
}

public enum RecommendationType
{
    Project,
    Job,
    Freelancer,
    Course,
    LearningPath,
    Service
}

public class AIModelConfig : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public decimal Temperature { get; set; } = 0.7m;
    public int MaxTokens { get; set; } = 2000;
    public decimal MatchThreshold { get; set; } = 0.7m;
    public decimal MinConfidenceScore { get; set; } = 0.5m;
    public bool IsActive { get; set; } = true;
    public bool IsDefault { get; set; }
    public int MaxRetries { get; set; } = 3;
    public int TimeoutSeconds { get; set; } = 30;
    public string Metadata { get; set; } = string.Empty;
}