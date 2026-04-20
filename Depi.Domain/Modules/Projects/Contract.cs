namespace DEPI.Domain.Entities.Projects;

using Depi.Domain.Modules.Projects.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Entities.Identity;

public class Contract : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid ProjectId { get;set; }
    public Guid? ProposalId { get;set; }
    public Guid FreelancerId { get;set; }
    public Guid ClientId { get;set; }
    public string Title { get;set; } = string.Empty;
    public ContractStatus Status { get;set; }
    public decimal TotalAmount { get;set; }
    public decimal PlatformFee { get;set; }
    public decimal FreelancerEarnings { get;set; }
    public DateTime? StartDate { get;set; }
    public DateTime? EndDate { get;set; }
    public int DurationDays { get;set; }
    public string? Terms { get;set; }
    public string? SpecialTerms { get;set; }
    public bool IsNda { get;set; }
    public decimal? HourlyRate { get;set; }
    public decimal? TotalHours { get;set; }
    public decimal? TotalHoursAmount { get;set; }
    public DateTime? LastActivityAt { get;set; }
    public string? CancellationReason { get;set; }
    public DateTime? CancelledAt { get;set; }
    public Guid? CancelledBy { get;set; }

    public Project? Project { get;set; }
    public virtual Proposal? Proposal { get;set; }
    public User? Freelancer { get;set; }
    public User? Client { get;set; }
    public ICollection<Milestone> Milestones { get;set; } = new HashSet<Milestone>();

    private Contract() { }

    public static Contract Create(
        Guid projectId,
        Guid? proposalId,
        Guid freelancerId,
        Guid clientId,
        string title,
        decimal totalAmount,
        decimal platformFee,
        string? terms = null,
        string? specialTerms = null,
        bool isNda = false)
    {
        if (projectId == Guid.Empty)
            throw new ArgumentException("Project ID is required", nameof(projectId));

        if (freelancerId == Guid.Empty)
            throw new ArgumentException("Freelancer ID is required", nameof(freelancerId));

        if (clientId == Guid.Empty)
            throw new ArgumentException("Client ID is required", nameof(clientId));

        if (totalAmount <= 0)
            throw new ArgumentException("Total amount must be greater than zero", nameof(totalAmount));

        var freelancerEarnings = totalAmount - platformFee;

        var contract = new Contract
        {
            ProjectId = projectId,
            ProposalId = proposalId,
            FreelancerId = freelancerId,
            ClientId = clientId,
            Title = title,
            TotalAmount = totalAmount,
            PlatformFee = platformFee,
            FreelancerEarnings = freelancerEarnings,
            Status = ContractStatus.Draft,
            Terms = terms,
            SpecialTerms = specialTerms,
            IsNda = isNda
        };

        return contract;
    }

    public void Start()
    {
        if (Status != ContractStatus.Draft)
            throw new InvalidOperationException("Only draft contracts can be started");

        Status = ContractStatus.Active;
        StartDate = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new ContractStartedEvent(Id, ProjectId, FreelancerId, ClientId, TotalAmount));
    }

    public void Pause(string reason)
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("Only active contracts can be paused");

        Status = ContractStatus.Paused;
        LastActivityAt = DateTime.UtcNow;
    }

    public void Resume()
    {
        if (Status != ContractStatus.Paused)
            throw new InvalidOperationException("Only paused contracts can be resumed");

        Status = ContractStatus.Active;
        LastActivityAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("Only active contracts can be completed");

        Status = ContractStatus.Completed;
        EndDate = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new ContractCompletedEvent(Id, ProjectId, FreelancerId, ClientId, FreelancerEarnings));
    }

    public void Cancel(string reason, Guid cancelledBy)
    {
        if (Status == ContractStatus.Completed)
            throw new InvalidOperationException("Completed contracts cannot be cancelled");

        Status = ContractStatus.Cancelled;
        CancellationReason = reason;
        CancelledBy = cancelledBy;
        CancelledAt = DateTime.UtcNow;
        EndDate = DateTime.UtcNow;
        
        RaiseDomainEvent(new ContractCancelledEvent(Id, ProjectId, reason, cancelledBy));
    }

    public void Dispute(string reason, Guid disputedBy)
    {
        if (Status != ContractStatus.Active && Status != ContractStatus.Completed)
            throw new InvalidOperationException("Cannot dispute this contract");

        Status = ContractStatus.InDispute;
        CancellationReason = reason;
        CancelledBy = disputedBy;
        LastActivityAt = DateTime.UtcNow;
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

public class ContractStartedEvent : DomainEventBase
{
    public Guid ContractId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }
    public Guid ClientId { get; }
    public decimal TotalAmount { get; }

    public override string EventType => nameof(ContractStartedEvent);

    public ContractStartedEvent(Guid contractId, Guid projectId, Guid freelancerId, Guid clientId, decimal totalAmount)
    {
        ContractId = contractId;
        ProjectId = projectId;
        FreelancerId = freelancerId;
        ClientId = clientId;
        TotalAmount = totalAmount;
    }
}

public class ContractCompletedEvent : DomainEventBase
{
    public Guid ContractId { get; }
    public Guid ProjectId { get; }
    public Guid FreelancerId { get; }
    public Guid ClientId { get; }
    public decimal FreelancerEarnings { get; }

    public override string EventType => nameof(ContractCompletedEvent);

    public ContractCompletedEvent(Guid contractId, Guid projectId, Guid freelancerId, Guid clientId, decimal freelancerEarnings)
    {
        ContractId = contractId;
        ProjectId = projectId;
        FreelancerId = freelancerId;
        ClientId = clientId;
        FreelancerEarnings = freelancerEarnings;
    }
}

public class ContractCancelledEvent : DomainEventBase
{
    public Guid ContractId { get; }
    public Guid ProjectId { get; }
    public string Reason { get; }
    public Guid CancelledBy { get; }

    public override string EventType => nameof(ContractCancelledEvent);

    public ContractCancelledEvent(Guid contractId, Guid projectId, string reason, Guid cancelledBy)
    {
        ContractId = contractId;
        ProjectId = projectId;
        Reason = reason;
        CancelledBy = cancelledBy;
    }
}
