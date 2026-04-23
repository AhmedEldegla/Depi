namespace DEPI.Domain.Entities.Contracts;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Enums;

public class Milestone : AuditableEntity
{
    public Guid ContractId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public MilestoneStatus Status { get; private set; } = MilestoneStatus.Pending;
    public DateTime? StartedAt { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? Deliverables { get; private set; }

    public virtual Contract? Contract { get; private set; }

    private Milestone() { }

    public static Milestone Create(
        Guid contractId,
        string title,
        string description,
        decimal amount,
        DateTime? dueDate = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("عنوان المرحلة مطلوب", nameof(title));

        return new Milestone
        {
            ContractId = contractId,
            Title = title.Trim(),
            Description = description?.Trim() ?? string.Empty,
            Amount = amount,
            DueDate = dueDate,
            Status = MilestoneStatus.Pending
        };
    }

    public void Start()
    {
        if (Status != MilestoneStatus.Pending)
            throw new InvalidOperationException("لا يمكن بدء مرحلة غير معلقه");

        Status = MilestoneStatus.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void Complete(string? deliverables = null)
    {
        if (Status != MilestoneStatus.InProgress)
            throw new InvalidOperationException("لا يمكن إكمال مرحلة غير نشطه");

        Status = MilestoneStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        Deliverables = deliverables;
    }

    public void Cancel()
    {
        if (Status == MilestoneStatus.Completed)
            throw new InvalidOperationException("لا يمكن إلغاء مرحلة مكتملة");

        Status = MilestoneStatus.Cancelled;
    }
}