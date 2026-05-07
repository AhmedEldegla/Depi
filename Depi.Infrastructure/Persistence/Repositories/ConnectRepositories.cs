using DEPI.Application.Repositories.Wallets;
using DEPI.Domain.Entities.Wallets;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ConnectRepository : Repository<Connect>, IConnectRepository
{
    public ConnectRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Connect>> GetActiveConnectsAsync()
    {
        return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
    }

    public async Task<Connect?> GetByAmountAsync(int amount)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Amount == amount && c.IsActive);
    }
}

public class ConnectPurchaseRepository : Repository<ConnectPurchase>, IConnectPurchaseRepository
{
    public ConnectPurchaseRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ConnectPurchase>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(p => p.UserId == userId).OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<List<ConnectPurchase>> GetPendingPurchasesAsync()
    {
        return await _dbSet.Where(p => p.Status == PurchaseStatus.Pending).ToListAsync();
    }

    public async Task<decimal> GetTotalSpentAsync(Guid userId)
    {
        return await _dbSet.Where(p => p.UserId == userId && p.Status == PurchaseStatus.Completed).SumAsync(p => p.AmountPaid);
    }
}

public class ConnectUsageRepository : Repository<ConnectUsage>, IConnectUsageRepository
{
    public ConnectUsageRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ConnectUsage>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(u => u.UserId == userId).OrderByDescending(u => u.CreatedAt).ToListAsync();
    }

    public async Task<List<ConnectUsage>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbSet.Where(u => u.ProjectId == projectId).ToListAsync();
    }

    public async Task<int> GetTotalUsedAsync(Guid userId)
    {
        return await _dbSet.Where(u => u.UserId == userId && !u.IsRefunded).SumAsync(u => u.ConnectsUsed);
    }
}

public class FreelancerSubscriptionRepository : Repository<FreelancerSubscription>, IFreelancerSubscriptionRepository
{
    public FreelancerSubscriptionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<FreelancerSubscription?> GetActiveSubscriptionAsync(Guid userId)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);
    }

    public async Task<List<FreelancerSubscription>> GetExpiredSubscriptionsAsync()
    {
        return await _dbSet.Where(s => s.Status == SubscriptionStatus.Expired).ToListAsync();
    }

    public async Task<List<FreelancerSubscription>> GetExpiringSoonAsync(int days)
    {
        var cutoff = DateTime.UtcNow.AddDays(days);
        return await _dbSet.Where(s => s.EndDate <= cutoff && s.EndDate > DateTime.UtcNow && s.Status == SubscriptionStatus.Active).ToListAsync();
    }
}
