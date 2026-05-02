using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Wallets;
using DEPI.Domain.Entities.Wallets;
using MediatR;

namespace DEPI.Application.UseCases.Connects;

public class EarningRuleResponse { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public int ConnectsAwarded { get; set; } public string Trigger { get; set; } = string.Empty; public int MaxPerDay { get; set; } }

public class ConnectEarningResponse { public Guid Id { get; set; } public int ConnectsEarned { get; set; } public string TriggerType { get; set; } = string.Empty; public string SourceDescription { get; set; } = string.Empty; public DateTime EarnedAt { get; set; } }

public class EarningSummaryResponse { public int TotalEarned { get; set; } public int TotalPurchased { get; set; } public int TotalSpent { get; set; } public int AvailableBalance { get; set; } public int EarnedToday { get; set; } public List<EarningRuleResponse> ActiveRules { get; set; } = new(); public List<ConnectEarningResponse> RecentEarnings { get; set; } = new(); }

public record TriggerEarningRequest(EarningTrigger Trigger, string Description, Guid? ProjectId, Guid? ReviewId, Guid? MessageId);

public record GetEarningRulesQuery : IRequest<List<EarningRuleResponse>>;
public record GetEarningSummaryQuery(Guid UserId) : IRequest<EarningSummaryResponse>;
public record TriggerEarningCommand(Guid UserId, TriggerEarningRequest Request) : IRequest<ConnectEarningResponse?>;
public record GetEarningHistoryQuery(Guid UserId) : IRequest<List<ConnectEarningResponse>>;

public class GetEarningRulesQueryHandler : IRequestHandler<GetEarningRulesQuery, List<EarningRuleResponse>>
{
    private readonly IConnectEarningRuleRepository _repo; private readonly IMapper _mapper;
    public GetEarningRulesQueryHandler(IConnectEarningRuleRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<EarningRuleResponse>> Handle(GetEarningRulesQuery r, CancellationToken ct) => _mapper.Map<List<EarningRuleResponse>>(await _repo.GetActiveRulesAsync());
}

public class GetEarningSummaryQueryHandler : IRequestHandler<GetEarningSummaryQuery, EarningSummaryResponse>
{
    private readonly IConnectEarningRepository _earningRepo; private readonly IConnectEarningRuleRepository _ruleRepo;
    private readonly IConnectPurchaseRepository _purchaseRepo; private readonly IConnectUsageRepository _usageRepo; private readonly IMapper _mapper;
    public GetEarningSummaryQueryHandler(IConnectEarningRepository eRepo, IConnectEarningRuleRepository rRepo, IConnectPurchaseRepository pRepo, IConnectUsageRepository uRepo, IMapper mapper)
    { _earningRepo = eRepo; _ruleRepo = rRepo; _purchaseRepo = pRepo; _usageRepo = uRepo; _mapper = mapper; }

    public async Task<EarningSummaryResponse> Handle(GetEarningSummaryQuery r, CancellationToken ct)
    {
        var uid = r.UserId.ToString();
        var todayStart = DateTime.UtcNow.Date;
        return new EarningSummaryResponse
        {
            TotalEarned = await _earningRepo.GetTotalEarnedAsync(uid),
            TotalSpent = await _usageRepo.GetTotalUsedAsync(uid),
            AvailableBalance = await _earningRepo.GetTotalEarnedAsync(uid) - await _usageRepo.GetTotalUsedAsync(uid),
            EarnedToday = await _earningRepo.GetEarnedInPeriodAsync(uid, todayStart),
            ActiveRules = _mapper.Map<List<EarningRuleResponse>>(await _ruleRepo.GetActiveRulesAsync()),
            RecentEarnings = _mapper.Map<List<ConnectEarningResponse>>((await _earningRepo.GetByUserIdAsync(uid)).Take(10))
        };
    }
}

public class TriggerEarningCommandHandler : IRequestHandler<TriggerEarningCommand, ConnectEarningResponse?>
{
    private readonly IConnectEarningRuleRepository _ruleRepo; private readonly IConnectEarningRepository _earningRepo; private readonly IMapper _mapper;
    public TriggerEarningCommandHandler(IConnectEarningRuleRepository rRepo, IConnectEarningRepository eRepo, IMapper mapper) { _ruleRepo = rRepo; _earningRepo = eRepo; _mapper = mapper; }

    public async Task<ConnectEarningResponse?> Handle(TriggerEarningCommand r, CancellationToken ct)
    {
        var rule = await _ruleRepo.GetByTriggerAsync(r.Request.Trigger);
        if (rule == null) return null;
        var uid = r.UserId.ToString();
        var todayEarned = await _earningRepo.GetTodayEarnedAsync(uid, r.Request.Trigger);
        if (rule.MaxPerDay > 0 && todayEarned >= rule.MaxPerDay * rule.ConnectsAwarded) return null;
        var earning = new ConnectEarning { UserId = uid, RuleId = rule.Id, ConnectsEarned = rule.ConnectsAwarded, TriggerType = rule.Trigger.ToString(), SourceDescription = r.Request.Description, RelatedProjectId = r.Request.ProjectId, RelatedReviewId = r.Request.ReviewId, RelatedMessageId = r.Request.MessageId, EarnedAt = DateTime.UtcNow };
        await _earningRepo.AddAsync(earning, ct);
        return _mapper.Map<ConnectEarningResponse>(earning);
    }
}

public class GetEarningHistoryQueryHandler : IRequestHandler<GetEarningHistoryQuery, List<ConnectEarningResponse>>
{
    private readonly IConnectEarningRepository _repo; private readonly IMapper _mapper;
    public GetEarningHistoryQueryHandler(IConnectEarningRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<ConnectEarningResponse>> Handle(GetEarningHistoryQuery r, CancellationToken ct) => _mapper.Map<List<ConnectEarningResponse>>(await _repo.GetByUserIdAsync(r.UserId.ToString()));
}
