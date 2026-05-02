using DEPI.Application.Repositories.Wallets;
using DEPI.Domain.Entities.Wallets;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ConnectEarningRuleRepository : Repository<ConnectEarningRule>, IConnectEarningRuleRepository
{
    public ConnectEarningRuleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ConnectEarningRule>> GetActiveRulesAsync()
        => await _dbSet.Where(r => r.IsActive).OrderBy(r => r.DisplayOrder).ToListAsync();

    public async Task<ConnectEarningRule?> GetByTriggerAsync(EarningTrigger trigger)
        => await _dbSet.FirstOrDefaultAsync(r => r.Trigger == trigger && r.IsActive);
}

public class ConnectEarningRepository : Repository<ConnectEarning>, IConnectEarningRepository
{
    public ConnectEarningRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<ConnectEarning>> GetByUserIdAsync(string userId)
        => await _dbSet.Where(e => e.UserId == userId).OrderByDescending(e => e.EarnedAt).ToListAsync();

    public async Task<int> GetTotalEarnedAsync(string userId)
        => await _dbSet.Where(e => e.UserId == userId).SumAsync(e => e.ConnectsEarned);

    public async Task<int> GetTodayEarnedAsync(string userId, EarningTrigger trigger)
    {
        var today = DateTime.UtcNow.Date;
        return await _dbSet.Where(e => e.UserId == userId && e.TriggerType == trigger.ToString() && e.EarnedAt >= today).SumAsync(e => e.ConnectsEarned);
    }

    public async Task<int> GetEarnedInPeriodAsync(string userId, DateTime since)
        => await _dbSet.Where(e => e.UserId == userId && e.EarnedAt >= since).SumAsync(e => e.ConnectsEarned);
}
