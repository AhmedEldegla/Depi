using DEPI.Application.Repositories.AIMatching;
using DEPI.Domain.Entities.AIMatching;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class SkillMatchRepository : Repository<SkillMatch>, ISkillMatchRepository
{
    public SkillMatchRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<SkillMatch>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbSet.Where(m => m.ProjectId == projectId).ToListAsync();
    }

    public async Task<List<SkillMatch>> GetByFreelancerIdAsync(string freelancerId)
    {
        var freelancerGuid = Guid.Parse(freelancerId);
        return await _dbSet.Where(m => m.FreelancerId == freelancerGuid.ToString()).ToListAsync();
    }

    public async Task<List<SkillMatch>> GetMatchesAboveThresholdAsync(Guid projectId, decimal threshold)
    {
        return await _dbSet.Where(m => m.ProjectId == projectId && m.MatchScore >= threshold).ToListAsync();
    }
}

public class ProjectMatchRepository : Repository<ProjectMatch>, IProjectMatchRepository
{
    public ProjectMatchRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ProjectMatch>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbSet.Where(m => m.ProjectId == projectId).OrderByDescending(m => m.OverallScore).ToListAsync();
    }

    public async Task<List<ProjectMatch>> GetByFreelancerIdAsync(string freelancerId)
    {
        var freelancerGuid = Guid.Parse(freelancerId);
        return await _dbSet.Where(m => m.FreelancerId == freelancerGuid.ToString()).ToListAsync();
    }

    public async Task<ProjectMatch?> GetBestMatchForProjectAsync(Guid projectId)
    {
        return await _dbSet.Where(m => m.ProjectId == projectId).OrderByDescending(m => m.OverallScore).FirstOrDefaultAsync();
    }

    public async Task<List<ProjectMatch>> GetTopMatchesAsync(Guid projectId, int count)
    {
        return await _dbSet.Where(m => m.ProjectId == projectId).OrderByDescending(m => m.OverallScore).Take(count).ToListAsync();
    }
}

public class JobMatchRepository : Repository<JobMatch>, IJobMatchRepository
{
    public JobMatchRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<JobMatch>> GetByJobIdAsync(Guid jobId)
    {
        return await _dbSet.Where(m => m.JobId == jobId).OrderByDescending(m => m.OverallScore).ToListAsync();
    }

    public async Task<List<JobMatch>> GetByFreelancerIdAsync(string freelancerId)
    {
        var freelancerGuid = Guid.Parse(freelancerId);
        return await _dbSet.Where(m => m.FreelancerId == freelancerGuid.ToString()).ToListAsync();
    }

    public async Task<JobMatch?> GetBestMatchForJobAsync(Guid jobId)
    {
        return await _dbSet.Where(m => m.JobId == jobId).OrderByDescending(m => m.OverallScore).FirstOrDefaultAsync();
    }

    public async Task<List<JobMatch>> GetTopMatchesAsync(Guid jobId, int count)
    {
        return await _dbSet.Where(m => m.JobId == jobId).OrderByDescending(m => m.OverallScore).Take(count).ToListAsync();
    }
}

public class FreelancerScoreRepository : Repository<FreelancerScore>, IFreelancerScoreRepository
{
    public FreelancerScoreRepository(ApplicationDbContext context) : base(context) { }

    public async Task<FreelancerScore?> GetByFreelancerIdAsync(string freelancerId)
    {
        var freelancerGuid = Guid.Parse(freelancerId);
        return await _dbSet.FirstOrDefaultAsync(s => s.FreelancerId == freelancerGuid.ToString());
    }

    public async Task<List<FreelancerScore>> GetTopScoredAsync(int count)
    {
        return await _dbSet.OrderByDescending(s => s.OverallScore).Take(count).ToListAsync();
    }
}

public class ScoringRuleRepository : Repository<ScoringRule>, IScoringRuleRepository
{
    public ScoringRuleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ScoringRule>> GetActiveRulesAsync()
    {
        return await _dbSet.Where(r => r.IsActive).OrderBy(r => r.DisplayOrder).ToListAsync();
    }

    public async Task<List<ScoringRule>> GetByTypeAsync(ScoringRuleType type)
    {
        return await _dbSet.Where(r => r.Type == type && r.IsActive).ToListAsync();
    }
}

public class RecommendationRepository : Repository<Recommendation>, IRecommendationRepository
{
    public RecommendationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Recommendation>> GetByUserIdAsync(string userId)
    {
        var userGuid = Guid.Parse(userId);
        return await _dbSet.Where(r => r.UserId == userGuid.ToString()).OrderByDescending(r => r.ConfidenceScore).ToListAsync();
    }

    public async Task<List<Recommendation>> GetActiveForUserAsync(string userId)
    {
        var userGuid = Guid.Parse(userId);
        return await _dbSet
            .Where(r => r.UserId == userGuid.ToString() && r.ExpiresAt > DateTime.UtcNow && !r.IsViewed)
            .OrderByDescending(r => r.ConfidenceScore)
            .ToListAsync();
    }

    public async Task<List<Recommendation>> GetByTypeAsync(string userId, RecommendationType type)
    {
        var userGuid = Guid.Parse(userId);
        return await _dbSet.Where(r => r.UserId == userGuid.ToString() && r.Type == type).ToListAsync();
    }
}

public class AIModelConfigRepository : Repository<AIModelConfig>, IAIModelConfigRepository
{
    public AIModelConfigRepository(ApplicationDbContext context) : base(context) { }

    public async Task<AIModelConfig?> GetDefaultAsync()
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.IsDefault && c.IsActive);
    }

    public async Task<AIModelConfig?> GetByProviderAsync(string provider)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Provider == provider && c.IsActive);
    }
}

public class AILogRepository : Repository<AILog>, IAILogRepository
{
    public AILogRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<AILog>> GetByUserIdAsync(string userId)
    {
        var userGuid = Guid.Parse(userId);
        return await _dbSet.Where(l => l.UserId == userGuid.ToString()).OrderByDescending(l => l.CreatedAt).ToListAsync();
    }

    public async Task<List<AILog>> GetByActionAsync(string action)
    {
        return await _dbSet.Where(l => l.Action == action).OrderByDescending(l => l.CreatedAt).ToListAsync();
    }

    public async Task<List<AILog>> GetRecentLogsAsync(int count)
    {
        return await _dbSet.OrderByDescending(l => l.CreatedAt).Take(count).ToListAsync();
    }
}
