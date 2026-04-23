namespace DEPI.Domain.Entities.Contracts;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Enums;

public class Contract : AuditableEntity
{
    public Guid ProjectId { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid FreelancerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public ContractStatus Status { get; private set; } = ContractStatus.Pending;
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? CancellationReason { get; private set; }
    public string? DisputeReason { get; private set; }

    public virtual Project? Project { get; private set; }
    public virtual User? Client { get; private set; }
    public virtual User? Freelancer { get; private set; }
    public virtual ICollection<Milestone> Milestones { get; private set; } = new List<Milestone>();

    private Contract() { }

    public static Contract Create(
        Guid projectId,
        Guid clientId,
        Guid freelancerId,
        decimal totalAmount)
    {
        return new Contract
        {
            ProjectId = projectId,
            ClientId = clientId,
            FreelancerId = freelancerId,
            TotalAmount = totalAmount,
            Status = ContractStatus.Pending
        };
    }

    public void Start()
    {
        if (Status != ContractStatus.Pending)
            throw new InvalidOperationException("لا يمكن بدء عقد غير معلق");

        Status = ContractStatus.Active;
        StartedAt = DateTime.UtcNow;
    }

    public void Pause()
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("لا يمكن إيقاف عقد غير نشط");

        Status = ContractStatus.Paused;
    }

    public void Resume()
    {
        if (Status != ContractStatus.Paused)
            throw new InvalidOperationException("لا يمكن استئناف عقد غير متوقف");

        Status = ContractStatus.Active;
    }

    public void Complete()
    {
        if (Status != ContractStatus.Active)
            throw new InvalidOperationException("لا يمكن إكمال عقد غير نشط");

        Status = ContractStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Cancel(string reason)
    {
        if (Status == ContractStatus.Completed)
            throw new InvalidOperationException("لا يمكن إلغاء عقد مكتمل");

        Status = ContractStatus.Cancelled;
        CancellationReason = reason;
    }

    public void OpenDispute(string reason)
    {
        if (Status != ContractStatus.Active && Status != ContractStatus.Paused)
            throw new InvalidOperationException("لا يمكن فتح نزاع على عقد غير نشط");

        Status = ContractStatus.Disputed;
        DisputeReason = reason;
    }
}