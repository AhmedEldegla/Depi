using DEPI.Application.Repositories.HeadHunters;
using DEPI.Domain.Entities.HeadHunters;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class HeadHunterRepository : Repository<HeadHunter>, IHeadHunterRepository
{
    public HeadHunterRepository(ApplicationDbContext context) : base(context) { }

    public async Task<HeadHunter?> GetByUserIdAsync(Guid userId)
        => await _dbSet.FirstOrDefaultAsync(h => h.UserId == userId);

    public async Task<List<HeadHunter>> GetActiveAsync()
        => await _dbSet.Where(h => h.Status == HeadHunterStatus.Active).OrderByDescending(h => h.SuccessRate).ToListAsync();

    public async Task<List<HeadHunter>> GetBySpecializationAsync(string specialization)
        => await _dbSet.Where(h => h.Specialization.Contains(specialization) && h.Status == HeadHunterStatus.Active).ToListAsync();

    public async Task<List<HeadHunter>> GetTopPerformersAsync(int count)
        => await _dbSet.Where(h => h.Status == HeadHunterStatus.Active).OrderByDescending(h => h.SuccessRate).Take(count).ToListAsync();
}

public class HeadHunterAssignmentRepository : Repository<HeadHunterAssignment>, IHeadHunterAssignmentRepository
{
    public HeadHunterAssignmentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<HeadHunterAssignment>> GetByHunterIdAsync(Guid hunterId)
        => await _dbSet.Where(a => a.HeadHunterId == hunterId).OrderByDescending(a => a.CreatedAt).ToListAsync();

    public async Task<List<HeadHunterAssignment>> GetActiveAssignmentsAsync()
        => await _dbSet.Where(a => a.Status == AssignmentStatus.Active).ToListAsync();

    public async Task<List<HeadHunterAssignment>> GetByClientIdAsync(Guid clientId)
    {
        return await _dbSet.Where(a => a.ClientId == clientId).OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<HeadHunterAssignment?> GetActiveAssignmentAsync(Guid hunterId, Guid clientId)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.HeadHunterId == hunterId && a.ClientId == clientId && a.Status == AssignmentStatus.Active);
    }
}

public class TalentRecommendationRepository : Repository<TalentRecommendation>, ITalentRecommendationRepository
{
    public TalentRecommendationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<TalentRecommendation>> GetByAssignmentIdAsync(Guid assignmentId)
        => await _dbSet.Where(r => r.AssignmentId == assignmentId).OrderByDescending(r => r.MatchScore).ToListAsync();

    public async Task<List<TalentRecommendation>> GetByHunterIdAsync(Guid hunterId)
        => await _dbSet.Where(r => r.Assignment.HeadHunterId == hunterId).ToListAsync();

    public async Task<List<TalentRecommendation>> GetPendingAsync(Guid assignmentId)
        => await _dbSet.Where(r => r.AssignmentId == assignmentId && r.Result == RecommendationResult.Pending).ToListAsync();

    public async Task<List<TalentRecommendation>> GetByUserIdAsync(Guid userId)
        => await _dbSet.Where(r => r.RecommendedUserId == userId).OrderByDescending(r => r.CreatedAt).ToListAsync();
}
