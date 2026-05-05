using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.AIMatching;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.AIMatching;

public interface ISkillMatchRepository : IRepository<SkillMatch>
{
    Task<List<SkillMatch>> GetByProjectIdAsync(Guid projectId);
    Task<List<SkillMatch>> GetByFreelancerIdAsync(Guid freelancerId);
    Task<List<SkillMatch>> GetMatchesAboveThresholdAsync(Guid projectId, decimal threshold);
}

public interface IProjectMatchRepository : IRepository<ProjectMatch>
{
    Task<List<ProjectMatch>> GetByProjectIdAsync(Guid projectId);
    Task<List<ProjectMatch>> GetByFreelancerIdAsync(Guid freelancerId);
    Task<ProjectMatch?> GetBestMatchForProjectAsync(Guid projectId);
    Task<List<ProjectMatch>> GetTopMatchesAsync(Guid projectId, int count);
}

public interface IJobMatchRepository : IRepository<JobMatch>
{
    Task<List<JobMatch>> GetByJobIdAsync(Guid jobId);
    Task<List<JobMatch>> GetByFreelancerIdAsync(Guid freelancerId);
    Task<JobMatch?> GetBestMatchForJobAsync(Guid jobId);
    Task<List<JobMatch>> GetTopMatchesAsync(Guid jobId, int count);
}

public interface IFreelancerScoreRepository : IRepository<FreelancerScore>
{
    Task<FreelancerScore?> GetByFreelancerIdAsync(Guid freelancerId);
    Task<List<FreelancerScore>> GetTopScoredAsync(int count);
}

public interface IScoringRuleRepository : IRepository<ScoringRule>
{
    Task<List<ScoringRule>> GetActiveRulesAsync();
    Task<List<ScoringRule>> GetByTypeAsync(ScoringRuleType type);
}

public interface IRecommendationRepository : IRepository<Recommendation>
{
    Task<List<Recommendation>> GetByUserIdAsync(Guid userId);
    Task<List<Recommendation>> GetActiveForUserAsync(Guid userId);
    Task<List<Recommendation>> GetByTypeAsync(Guid userId, RecommendationType type);
}

public interface IAIModelConfigRepository : IRepository<AIModelConfig>
{
    Task<AIModelConfig?> GetDefaultAsync();
    Task<AIModelConfig?> GetByProviderAsync(string provider);
}

public interface IAILogRepository : IRepository<AILog>
{
    Task<List<AILog>> GetByUserIdAsync(Guid userId);
    Task<List<AILog>> GetByActionAsync(string action);
    Task<List<AILog>> GetRecentLogsAsync(int count);
}