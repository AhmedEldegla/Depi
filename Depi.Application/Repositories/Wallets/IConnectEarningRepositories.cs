using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Wallets;

public interface IConnectEarningRuleRepository : IRepository<ConnectEarningRule>
{
    Task<List<ConnectEarningRule>> GetActiveRulesAsync();
    Task<ConnectEarningRule?> GetByTriggerAsync(EarningTrigger trigger);
}

public interface IConnectEarningRepository : IRepository<ConnectEarning>
{
    Task<List<ConnectEarning>> GetByUserIdAsync(string userId);
    Task<int> GetTotalEarnedAsync(string userId);
    Task<int> GetTodayEarnedAsync(string userId, EarningTrigger trigger);
    Task<int> GetEarnedInPeriodAsync(string userId, DateTime since);
}
