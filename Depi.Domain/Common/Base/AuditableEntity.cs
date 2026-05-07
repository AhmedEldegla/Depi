namespace DEPI.Domain.Common.Base;

public abstract class AuditableEntity : Entity
{
    public Guid CreatedBy { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    public Guid? DeletedBy { get; protected set; }

    public void MarkAsDeleted(Guid deletedBy)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }

    public void UndoDelete()
    {
        IsDeleted = false;
        DeletedAt = null;
        DeletedBy = null;
    }

    public void SetCreator(Guid creatorId)
    {
        CreatedBy = creatorId;
    }

    public void SetUpdater(Guid updaterId)
    {
        UpdatedBy = updaterId;
        UpdatedAt = DateTime.UtcNow;
    }
}