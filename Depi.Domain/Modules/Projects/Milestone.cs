namespace DEPI.Domain.Entities.Projects;

using Depi.Domain.Modules.Projects.Enums;
using DEPI.Domain.Common.Base;

public class Milestone : AuditableEntity
{
    public Guid ContractId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public int Order { get;set; }
    public decimal Amount { get;set; }
    public MilestoneStatus Status { get;set; }
    public DateTime? DueDate { get;set; }
    public DateTime? StartedAt { get;set; }
    public DateTime? SubmittedAt { get;set; }
    public DateTime? ApprovedAt { get;set; }
    public DateTime? RejectedAt { get;set; }
    public string? RejectionReason { get;set; }
    public bool IsPaymentReleased { get;set; }
    public DateTime? PaymentReleasedAt { get;set; }

    public virtual Contract Contract { get;set; }
    public ICollection<Deliverable> Deliverables { get;set; } = new HashSet<Deliverable>();

    private Milestone() { }

    public static Milestone Create(
        Guid contractId,
        string title,
        string description,
        decimal amount,
        int order,
        DateTime? dueDate = null)
    {
        if (contractId == Guid.Empty)
            throw new ArgumentException("Contract ID is required", nameof(contractId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));

        return new Milestone
        {
            ContractId = contractId,
            Title = title.Trim(),
            Description = description.Trim(),
            Amount = amount,
            Order = order,
            DueDate = dueDate,
            Status = MilestoneStatus.Pending
        };
    }

    public void Start()
    {
        if (Status != MilestoneStatus.Pending)
            throw new InvalidOperationException("Can only start pending milestones");

        Status = MilestoneStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void Submit()
    {
        if (Status != MilestoneStatus.InProgress)
            throw new InvalidOperationException("Can only submit in-progress milestones");

        Status = MilestoneStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
    }

    public void Approve()
    {
        if (Status != MilestoneStatus.Submitted)
            throw new InvalidOperationException("Can only approve submitted milestones");

        Status = MilestoneStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
        IsPaymentReleased = true;
        PaymentReleasedAt = DateTime.UtcNow;
    }

    public void Reject(string reason)
    {
        if (Status != MilestoneStatus.Submitted)
            throw new InvalidOperationException("Can only reject submitted milestones");

        Status = MilestoneStatus.NeedsRevision;
        RejectedAt = DateTime.UtcNow;
        RejectionReason = reason;
    }

    public void Cancel(string reason)
    {
        if (Status == MilestoneStatus.Approved || Status == MilestoneStatus.Cancelled)
            throw new InvalidOperationException("Cannot cancel this milestone");

        Status = MilestoneStatus.Cancelled;
    }
}

public class Deliverable : AuditableEntity
{
    public Guid MilestoneId { get;set; }
    public Guid ContractId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Description { get;set; } = string.Empty;
    public Guid? MediaId { get;set; }
    public DeliverableStatus Status { get;set; }
    public DateTime? SubmittedAt { get;set; }
    public DateTime? ApprovedAt { get;set; }
    public DateTime? RejectedAt { get;set; }
    public string? RejectionReason { get;set; }
    public string? RevisionNote { get;set; }
    public int RevisionCount { get;set; }

    public virtual Milestone Milestone { get;set; }
    public virtual Media.Media? Media { get;set; }

    private Deliverable() { }

    public static Deliverable Create(
        Guid milestoneId,
        Guid contractId,
        string title,
        string description,
        Guid? mediaId = null)
    {
        return new Deliverable
        {
            MilestoneId = milestoneId,
            ContractId = contractId,
            Title = title.Trim(),
            Description = description.Trim(),
            MediaId = mediaId,
            Status = DeliverableStatus.Pending
        };
    }

    public void Submit(Guid? mediaId = null)
    {
        if (Status != DeliverableStatus.Pending && Status != DeliverableStatus.NeedsRevision)
            throw new InvalidOperationException("Cannot submit this deliverable");

        Status = DeliverableStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        
        if (mediaId.HasValue)
            MediaId = mediaId;

        if (Status == DeliverableStatus.NeedsRevision)
            RevisionCount++;
    }

    public void Approve()
    {
        if (Status != DeliverableStatus.Submitted)
            throw new InvalidOperationException("Can only approve submitted deliverables");

        Status = DeliverableStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
    }

    public void Reject(string reason)
    {
        if (Status != DeliverableStatus.Submitted)
            throw new InvalidOperationException("Can only reject submitted deliverables");

        Status = DeliverableStatus.NeedsRevision;
        RejectedAt = DateTime.UtcNow;
        RejectionReason = reason;
        RevisionNote = reason;
    }
}
