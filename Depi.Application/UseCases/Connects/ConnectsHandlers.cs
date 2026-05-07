using DEPI.Application.Common;
using DEPI.Application.Repositories.Wallets;
using DEPI.Domain.Entities.Wallets;
using MediatR;

namespace DEPI.Application.UseCases.Connects;

public class ConnectPackResponse
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsBonus { get; set; }
}

public class ConnectBalanceResponse
{
    public int TotalPurchased { get; set; }
    public int TotalUsed { get; set; }
    public int BonusConnects { get; set; }
    public int Available { get; set; }
    public decimal TotalSpent { get; set; }
}

public class ConnectPurchaseResponse
{
    public Guid Id { get; set; }
    public string PackDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int TotalConnects { get; set; }
    public decimal AmountPaid { get; set; }
    public string Currency { get; set; } = string.Empty;
    public PurchaseStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ConnectUsageResponse
{
    public Guid Id { get; set; }
    public int ConnectsUsed { get; set; }
    public string UsageType { get; set; } = string.Empty;
    public Guid? ProjectId { get; set; }
    public Guid? JobId { get; set; }
    public bool IsRefunded { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record PurchaseConnectsRequest(Guid ConnectPackId, string PaymentMethod);
public record SpendConnectsRequest(string UsageType, Guid? ProjectId, Guid? JobId, int ConnectsUsed);

public record GetConnectPacksQuery : IRequest<List<ConnectPackResponse>>;
public record GetConnectBalanceQuery(Guid UserId) : IRequest<ConnectBalanceResponse>;
public record GetConnectHistoryQuery(Guid UserId) : IRequest<List<ConnectUsageResponse>>;
public record PurchaseConnectsCommand(Guid UserId, PurchaseConnectsRequest Request) : IRequest<ConnectPurchaseResponse>;

public class GetConnectPacksQueryHandler : IRequestHandler<GetConnectPacksQuery, List<ConnectPackResponse>>
{
    private readonly IConnectRepository _repo;
    public GetConnectPacksQueryHandler(IConnectRepository repo) => _repo = repo;

    public async Task<List<ConnectPackResponse>> Handle(GetConnectPacksQuery r, CancellationToken ct)
    {
        var packs = await _repo.GetActiveConnectsAsync();
        return packs.Select(p => new ConnectPackResponse
        {
            Id = p.Id, Amount = p.Amount, Description = p.Description,
            Price = p.Price, IsBonus = p.IsBonus
        }).ToList();
    }
}

public class GetConnectBalanceQueryHandler : IRequestHandler<GetConnectBalanceQuery, ConnectBalanceResponse>
{
    private readonly IConnectPurchaseRepository _purchaseRepo;
    private readonly IConnectUsageRepository _usageRepo;
    public GetConnectBalanceQueryHandler(IConnectPurchaseRepository purchaseRepo, IConnectUsageRepository usageRepo)
    { _purchaseRepo = purchaseRepo; _usageRepo = usageRepo; }

    public async Task<ConnectBalanceResponse> Handle(GetConnectBalanceQuery r, CancellationToken ct)
    {
        var userId = r.UserId;
        var totalUsed = await _usageRepo.GetTotalUsedAsync(userId);
        var totalSpent = await _purchaseRepo.GetTotalSpentAsync(userId);

        return new ConnectBalanceResponse
        {
            TotalPurchased = 0,
            TotalUsed = totalUsed,
            BonusConnects = 0,
            Available = 0,
            TotalSpent = totalSpent
        };
    }
}

public class GetConnectHistoryQueryHandler : IRequestHandler<GetConnectHistoryQuery, List<ConnectUsageResponse>>
{
    private readonly IConnectUsageRepository _repo;
    public GetConnectHistoryQueryHandler(IConnectUsageRepository repo) => _repo = repo;

    public async Task<List<ConnectUsageResponse>> Handle(GetConnectHistoryQuery r, CancellationToken ct)
    {
        var usages = await _repo.GetByUserIdAsync(r.UserId);
        return usages.Select(u => new ConnectUsageResponse
        {
            Id = u.Id, ConnectsUsed = u.ConnectsUsed, UsageType = u.UsageType,
            ProjectId = u.ProjectId, JobId = u.JobId, IsRefunded = u.IsRefunded,
            CreatedAt = u.CreatedAt
        }).ToList();
    }
}

public class PurchaseConnectsCommandHandler : IRequestHandler<PurchaseConnectsCommand, ConnectPurchaseResponse>
{
    private readonly IConnectRepository _connectRepo;
    private readonly IConnectPurchaseRepository _purchaseRepo;
    public PurchaseConnectsCommandHandler(IConnectRepository connectRepo, IConnectPurchaseRepository purchaseRepo)
    { _connectRepo = connectRepo; _purchaseRepo = purchaseRepo; }

    public async Task<ConnectPurchaseResponse> Handle(PurchaseConnectsCommand r, CancellationToken ct)
    {
        var pack = await _connectRepo.GetByIdAsync(r.Request.ConnectPackId, ct)
            ?? throw new KeyNotFoundException(Errors.NotFound("Connect pack"));

        var purchase = new ConnectPurchase
        {
            UserId = r.UserId,
            ConnectId = pack.Id,
            Quantity = 1,
            FreeConnects = pack.IsBonus ? pack.Amount : 0,
            TotalConnects = pack.Amount,
            AmountPaid = pack.Price,
            Currency = "USD",
            PaymentMethod = r.Request.PaymentMethod,
            TransactionId = Guid.NewGuid().ToString(),
            Status = PurchaseStatus.Completed
        };
        await _purchaseRepo.AddAsync(purchase, ct);

        return new ConnectPurchaseResponse
        {
            Id = purchase.Id, PackDescription = pack.Description,
            Quantity = purchase.Quantity, TotalConnects = purchase.TotalConnects,
            AmountPaid = purchase.AmountPaid, Currency = purchase.Currency,
            Status = purchase.Status, CreatedAt = purchase.CreatedAt
        };
    }
}
