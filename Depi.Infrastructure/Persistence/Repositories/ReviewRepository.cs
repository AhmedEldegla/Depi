using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Reviews;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Review>> GetByRevieweeIdAsync(Guid revieweeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.RevieweeId == revieweeId)
            .Include(r => r.Reviewer)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Review>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.ProjectId == projectId)
            .Include(r => r.Reviewer)
            .Include(r => r.Reviewee)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Review>> GetByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(r => r.ContractId == contractId)
            .Include(r => r.Reviewer)
            .Include(r => r.Reviewee)
            .ToListAsync(cancellationToken);
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(r => r.Reviewer)
            .Include(r => r.Reviewee)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<decimal> GetAverageRatingAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var reviews = await _dbSet
            .Where(r => r.RevieweeId == userId)
            .ToListAsync(cancellationToken);

        if (!reviews.Any())
            return 0;

        return (decimal)reviews.Average(r => r.Rating);
    }

    public async Task<int> GetReviewCountAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(r => r.RevieweeId == userId, cancellationToken);
    }
}