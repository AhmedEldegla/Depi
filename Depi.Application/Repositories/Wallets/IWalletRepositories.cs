using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Wallets;

public interface IConnectRepository : IRepository<Connect>
{
    Task<List<Connect>> GetActiveConnectsAsync();
    Task<Connect?> GetByAmountAsync(int amount);
}

public interface IConnectPurchaseRepository : IRepository<ConnectPurchase>
{
    Task<List<ConnectPurchase>> GetByUserIdAsync(string userId);
    Task<List<ConnectPurchase>> GetPendingPurchasesAsync();
    Task<decimal> GetTotalSpentAsync(string userId);
}

public interface IConnectUsageRepository : IRepository<ConnectUsage>
{
    Task<List<ConnectUsage>> GetByUserIdAsync(string userId);
    Task<List<ConnectUsage>> GetByProjectIdAsync(Guid projectId);
    Task<int> GetTotalUsedAsync(string userId);
}

public interface IFreelancerSubscriptionRepository : IRepository<FreelancerSubscription>
{
    Task<FreelancerSubscription?> GetActiveSubscriptionAsync(string userId);
    Task<List<FreelancerSubscription>> GetExpiredSubscriptionsAsync();
    Task<List<FreelancerSubscription>> GetExpiringSoonAsync(int days);
}