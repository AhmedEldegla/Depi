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
    Task<List<ConnectEarning>> GetByUserIdAsync(Guid userId);
    Task<int> GetTotalEarnedAsync(Guid userId);
    Task<int> GetTodayEarnedAsync(Guid userId, EarningTrigger trigger);
    Task<int> GetEarnedInPeriodAsync(Guid userId, DateTime since);
}
