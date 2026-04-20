namespace DEPI.Domain.Entities.Payments;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;

public class Escrow : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid ProjectId { get;set; }
    public Guid ContractId { get;set; }
    public Guid ClientWalletId { get;set; }
    public Guid FreelancerWalletId { get;set; }
    public Guid MilestoneId { get;set; }
    public decimal Amount { get;set; }
    public decimal PlatformFee { get;set; }
    public decimal NetAmount => Amount - PlatformFee;
    public decimal ServiceFee { get;set; }
    public string Currency { get;set; } = "USD";
    public EscrowStatus Status { get;set; }
    public DateTime? ReleasedAt { get;set; }
    public DateTime? RefundedAt { get;set; }
    public string? ReleaseReason { get;set; }
    public string? RefundReason { get;set; }

    public Projects.Project Project { get;set; }
    public Projects.Contract Contract { get;set; }
    public virtual Wallet ClientWallet { get;set; }
    public virtual Wallet FreelancerWallet { get;set; }
    public Projects.Milestone Milestone { get;set; }
    public ICollection<Transaction> Transactions { get;set; } = new HashSet<Transaction>();

    private Escrow() { }

    public static Escrow Create(
        Guid projectId,
        Guid contractId,
        Guid clientWalletId,
        Guid freelancerWalletId,
        Guid milestoneId,
        decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Escrow amount must be positive", nameof(amount));

        if (amount < 5)
            throw new ArgumentException("Minimum escrow amount is $5", nameof(amount));

        if (clientWalletId == freelancerWalletId)
            throw new ArgumentException("Client and freelancer wallets must be different");

        var escrow = new Escrow
        {
            ProjectId = projectId,
            ContractId = contractId,
            ClientWalletId = clientWalletId,
            FreelancerWalletId = freelancerWalletId,
            MilestoneId = milestoneId,
            Amount = amount,
            PlatformFee = CalculatePlatformFee(amount),
            ServiceFee = CalculateServiceFee(amount),
            Currency = "USD",
            Status = EscrowStatus.Funded
        };

        escrow.RaiseDomainEvent(new EscrowCreatedEvent(escrow.Id, projectId, contractId, amount));
        return escrow;
    }

    public void Release(string reason = "Milestone completed")
    {
        if (Status != EscrowStatus.Funded)
            throw new InvalidOperationException("Can only release funded escrow");

        Status = EscrowStatus.Released;
        ReleasedAt = DateTime.UtcNow;
        ReleaseReason = reason;

        RaiseDomainEvent(new EscrowReleasedEvent(Id, ProjectId, FreelancerWalletId, NetAmount, reason));
    }

    public void Cancel(string reason)
    {
        if (Status == EscrowStatus.Released)
            throw new InvalidOperationException("Cannot cancel released escrow");

        if (Status == EscrowStatus.Refunded)
            throw new InvalidOperationException("Escrow already refunded");

        Status = EscrowStatus.Refunded;
        RefundedAt = DateTime.UtcNow;
        RefundReason = reason;

        RaiseDomainEvent(new EscrowRefundedEvent(Id, ProjectId, ClientWalletId, Amount, reason));
    }

    public void Dispute(string reason)
    {
        if (Status != EscrowStatus.Funded)
            throw new InvalidOperationException("Can only dispute funded escrow");

        Status = EscrowStatus.InDispute;

        RaiseDomainEvent(new EscrowDisputedEvent(Id, ProjectId, reason));
    }

    public void ResolveInFavorOfFreelancer(string reason)
    {
        if (Status != EscrowStatus.InDispute)
            throw new InvalidOperationException("Can only resolve disputed escrow");

        Status = EscrowStatus.Released;
        ReleasedAt = DateTime.UtcNow;
        ReleaseReason = $"Dispute resolved in favor of freelancer: {reason}";

        RaiseDomainEvent(new EscrowReleasedEvent(Id, ProjectId, FreelancerWalletId, NetAmount, reason));
    }

    public void ResolveInFavorOfClient(string reason)
    {
        if (Status != EscrowStatus.InDispute)
            throw new InvalidOperationException("Can only resolve disputed escrow");

        Status = EscrowStatus.Refunded;
        RefundedAt = DateTime.UtcNow;
        RefundReason = $"Dispute resolved in favor of client: {reason}";

        RaiseDomainEvent(new EscrowRefundedEvent(Id, ProjectId, ClientWalletId, Amount, reason));
    }

    private static decimal CalculatePlatformFee(decimal amount)
    {
        return Math.Round(amount * 0.10m, 2);
    }

    private static decimal CalculateServiceFee(decimal amount)
    {
        return Math.Round(amount * 0.02m, 2);
    }

    private void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public enum EscrowStatus
{
    Pending = 0,
    Funded = 1,
    Released = 2,
    Refunded = 3,
    InDispute = 4,
    Cancelled = 5
}

public class EscrowCreatedEvent : DomainEventBase
{
    public Guid EscrowId { get; }
    public Guid ProjectId { get; }
    public Guid ContractId { get; }
    public decimal Amount { get; }

    public override string EventType => nameof(EscrowCreatedEvent);

    public EscrowCreatedEvent(Guid escrowId, Guid projectId, Guid contractId, decimal amount)
    {
        EscrowId = escrowId;
        ProjectId = projectId;
        ContractId = contractId;
        Amount = amount;
    }
}

public class EscrowReleasedEvent : DomainEventBase
{
    public Guid EscrowId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerWalletId { get; }
    public decimal Amount { get; }
    public string Reason { get; }

    public override string EventType => nameof(EscrowReleasedEvent);

    public EscrowReleasedEvent(Guid escrowId, Guid projectId, Guid freelancerWalletId, decimal amount, string reason)
    {
        EscrowId = escrowId;
        ProjectId = projectId;
        FreelancerWalletId = freelancerWalletId;
        Amount = amount;
        Reason = reason;
    }
}

public class EscrowRefundedEvent : DomainEventBase
{
    public Guid EscrowId { get; }
    public Guid ProjectId { get; }
    public Guid ClientWalletId { get; }
    public decimal Amount { get; }
    public string Reason { get; }

    public override string EventType => nameof(EscrowRefundedEvent);

    public EscrowRefundedEvent(Guid escrowId, Guid projectId, Guid clientWalletId, decimal amount, string reason)
    {
        EscrowId = escrowId;
        ProjectId = projectId;
        ClientWalletId = clientWalletId;
        Amount = amount;
        Reason = reason;
    }
}

public class EscrowDisputedEvent : DomainEventBase
{
    public Guid EscrowId { get; }
    public Guid ProjectId { get; }
    public string Reason { get; }

    public override string EventType => nameof(EscrowDisputedEvent);

    public EscrowDisputedEvent(Guid escrowId, Guid projectId, string reason)
    {
        EscrowId = escrowId;
        ProjectId = projectId;
        Reason = reason;
    }
}
