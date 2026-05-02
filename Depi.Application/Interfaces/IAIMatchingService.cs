using DEPI.Domain.Entities.AIMatching;

namespace DEPI.Application.Interfaces;

public interface IAIMatchingService
{
    Task<ProjectMatchResult> MatchFreelancersToProjectAsync(Guid projectId);
    Task<JobMatchResult> MatchFreelancersToJobAsync(Guid jobId);
    Task<List<ProjectMatchResult>> GetTopProjectMatchesAsync(Guid projectId, int count = 10);
    Task<List<JobMatchResult>> GetTopJobMatchesAsync(Guid jobId, int count = 10);
    Task<decimal> CalculateProjectMatchScoreAsync(Guid projectId, string freelancerId);
    Task<decimal> CalculateJobMatchScoreAsync(Guid jobId, string freelancerId);
}

public interface IFreelancerScoringService
{
    Task<FreelancerScoreResult> CalculateFreelancerScoreAsync(string freelancerId);
    Task<FreelancerScoreResult> GetOrCalculateScoreAsync(string freelancerId);
    Task<List<FreelancerScoreResult>> GetTopScoredFreelancersAsync(int count = 100);
    Task<Dictionary<string, decimal>> GetScoreBreakdownAsync(string freelancerId);
}

public interface IRecommendationService
{
    Task<List<RecommendationResult>> GetPersonalizedRecommendationsAsync(string userId);
    Task<List<RecommendationResult>> GetProjectRecommendationsForFreelancerAsync(string freelancerId);
    Task<List<RecommendationResult>> GetJobRecommendationsForFreelancerAsync(string freelancerId);
    Task<List<RecommendationResult>> GetCourseRecommendationsForUserAsync(string userId);
    Task RecordRecommendationClickAsync(Guid recommendationId);
    Task<decimal> GetRecommendationConfidenceAsync(string userId, DEPI.Domain.Entities.AIMatching.RecommendationType type);
}

public interface IAIAnalysisService
{
    Task<string> AnalyzeProposalAsync(string coverLetter, Guid projectId);
    Task<string> AnalyzeFreelancerProfileAsync(string freelancerId);
    Task<string> GenerateProjectDescriptionAsync(string title, string category);
    Task<string> EvaluatePortfolioItemAsync(string description, List<string> skills);
    Task<decimal> CalculateSkillProficiencyAsync(string freelancerId, Guid skillId);
    Task<string> GenerateMatchReasoningAsync(MatchContext context);
}

public interface IAIModelConfigService
{
    Task<AIModelConfigResult> GetActiveConfigurationAsync();
    Task<AIModelConfigResult> UpdateConfigurationAsync(AIModelConfigUpdate update);
    Task<decimal> GetMatchThresholdAsync();
    Task<bool> ValidateConfigurationAsync();
}

public class ProjectMatchResult
{
    public Guid ProjectId { get; set; }
    public string FreelancerId { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal BudgetScore { get; set; }
    public decimal AvailabilityScore { get; set; }
    public decimal ReputationScore { get; set; }
    public string AIReasoning { get; set; } = string.Empty;
    public int Rank { get; set; }
}

public class JobMatchResult
{
    public Guid JobId { get; set; }
    public string FreelancerId { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal SalaryScore { get; set; }
    public decimal LocationScore { get; set; }
    public string AIReasoning { get; set; } = string.Empty;
    public int Rank { get; set; }
}

public class FreelancerScoreResult
{
    public string FreelancerId { get; set; } = string.Empty;
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
}

public class RecommendationResult
{
    public Guid Id { get; set; }
    public RecommendationType Type { get; set; }
    public Guid TargetId { get; set; }
    public string TargetName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public decimal ConfidenceScore { get; set; }
    public string Context { get; set; } = string.Empty;
}

// RecommendationType is in DEPI.Domain.Entities.AIMatching

public class MatchContext
{
    public Guid ProjectId { get; set; }
    public string FreelancerId { get; set; } = string.Empty;
    public decimal OverallScore { get; set; }
    public decimal SkillScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public List<string> MatchingSkills { get; set; } = new();
    public List<string> MissingSkills { get; set; } = new();
    public string FreelancerStrengths { get; set; } = string.Empty;
    public string FreelancerWeaknesses { get; set; } = string.Empty;
}

public class AIModelConfigResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string ModelId { get; set; } = string.Empty;
    public decimal Temperature { get; set; }
    public int MaxTokens { get; set; }
    public decimal MatchThreshold { get; set; }
    public bool IsActive { get; set; }
}

public class AIModelConfigUpdate
{
    public decimal? Temperature { get; set; }
    public int? MaxTokens { get; set; }
    public decimal? MatchThreshold { get; set; }
    public bool? IsActive { get; set; }
}