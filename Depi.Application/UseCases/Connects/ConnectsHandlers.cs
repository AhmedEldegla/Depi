using DEPI.Application.Common;
using DEPI.Application.DTOs.Connects; 
using DEPI.Application.Repositories.Wallets;
using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Enums;
using MediatR;

namespace DEPI.Application.UseCases.Connects;


public record GetConnectPacksQuery : IRequest<List<ConnectPackResponse>>;
public record GetConnectBalanceQuery(Guid UserId) : IRequest<ConnectBalanceResponse>;
public record GetConnectHistoryQuery(Guid UserId) : IRequest<List<ConnectUsageResponse>>;
public record PurchaseConnectsCommand(Guid UserId, PurchaseConnectsRequest Request) : IRequest<ConnectPurchaseResponse>;

// Handlers
public class GetConnectPacksQueryHandler : IRequestHandler<GetConnectPacksQuery, List<ConnectPackResponse>>
{
    private readonly IConnectRepository _repo;
    public GetConnectPacksQueryHandler(IConnectRepository repo) => _repo = repo;
    public async Task<List<ConnectPackResponse>> Handle(GetConnectPacksQuery r, CancellationToken ct)
    {
        var packs = await _repo.GetActiveConnectsAsync();
        return packs.Select(p => new ConnectPackResponse { Id = p.Id, Amount = p.Amount, Description = p.Description, Price = p.Price, IsBonus = p.IsBonus }).ToList();
    }
}

public class GetConnectBalanceQueryHandler : IRequestHandler<GetConnectBalanceQuery, ConnectBalanceResponse>
{
    private readonly IConnectPurchaseRepository _purchaseRepo;
    private readonly IConnectUsageRepository _usageRepo;
    public GetConnectBalanceQueryHandler(IConnectPurchaseRepository purchaseRepo, IConnectUsageRepository usageRepo) { _purchaseRepo = purchaseRepo; _usageRepo = usageRepo; }
    public async Task<ConnectBalanceResponse> Handle(GetConnectBalanceQuery r, CancellationToken ct)
    {
        var totalUsed = await _usageRepo.GetTotalUsedAsync(r.UserId);
        var totalSpent = await _purchaseRepo.GetTotalSpentAsync(r.UserId);
        return new ConnectBalanceResponse { TotalPurchased = 0, TotalUsed = totalUsed, BonusConnects = 0, Available = 0, TotalSpent = totalSpent };
    }
}

public class GetConnectHistoryQueryHandler : IRequestHandler<GetConnectHistoryQuery, List<ConnectUsageResponse>>
{
    private readonly IConnectUsageRepository _repo;
    public GetConnectHistoryQueryHandler(IConnectUsageRepository repo) => _repo = repo;
    public async Task<List<ConnectUsageResponse>> Handle(GetConnectHistoryQuery r, CancellationToken ct)
    {
        var usages = await _repo.GetByUserIdAsync(r.UserId);
        return usages.Select(u => new ConnectUsageResponse { Id = u.Id, ConnectsUsed = u.ConnectsUsed, UsageType = u.UsageType, ProjectId = u.ProjectId, JobId = u.JobId, IsRefunded = u.IsRefunded, CreatedAt = u.CreatedAt }).ToList();
    }
}

public class PurchaseConnectsCommandHandler : IRequestHandler<PurchaseConnectsCommand, ConnectPurchaseResponse>
{
    private readonly IConnectRepository _connectRepo;
    private readonly IConnectPurchaseRepository _purchaseRepo;
    public PurchaseConnectsCommandHandler(IConnectRepository connectRepo, IConnectPurchaseRepository purchaseRepo) { _connectRepo = connectRepo; _purchaseRepo = purchaseRepo; }
    public async Task<ConnectPurchaseResponse> Handle(PurchaseConnectsCommand r, CancellationToken ct)
    {
        var pack = await _connectRepo.GetByIdAsync(r.Request.ConnectPackId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Connect pack"));
        var purchase = new ConnectPurchase { UserId = r.UserId, ConnectId = pack.Id, Quantity = 1, FreeConnects = pack.IsBonus ? pack.Amount : 0, TotalConnects = pack.Amount, AmountPaid = pack.Price, Currency = "USD", PaymentMethod = r.Request.PaymentMethod, TransactionId = Guid.NewGuid().ToString(), Status = PurchaseStatus.Completed };
        await _purchaseRepo.AddAsync(purchase, ct);
        return new ConnectPurchaseResponse { Id = purchase.Id, PackDescription = pack.Description, Quantity = purchase.Quantity, TotalConnects = purchase.TotalConnects, AmountPaid = purchase.AmountPaid, Currency = purchase.Currency, Status = purchase.Status, CreatedAt = purchase.CreatedAt };
    }
}