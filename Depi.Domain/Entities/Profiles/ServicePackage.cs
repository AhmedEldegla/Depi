namespace DEPI.Domain.Entities.Profiles;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Shared;

public class ServicePackage : AuditableEntity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int DeliveryDays { get; private set; }
    public int Revisions { get; private set; }
    public bool IsActive { get; private set; } = true;
    public bool IsFeatured { get; private set; }
    public Guid? CurrencyId { get; private set; }

    public virtual User? User { get; private set; }
    public virtual Currency? Currency { get; private set; }

    private ServicePackage() { }

    public static ServicePackage Create(
        Guid userId,
        string name,
        string description,
        decimal price,
        int deliveryDays,
        int revisions,
        Guid? currencyId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("الاسم مطلوب", nameof(name));
        if (price < 0)
            throw new ArgumentException("السعر يجب أن يكون إيجابياً", nameof(price));
        if (deliveryDays < 1)
            throw new ArgumentException("أيام التسليم يجب أن تكون أكبر من صفر", nameof(deliveryDays));
        if (revisions < 0)
            throw new ArgumentException("المراجعات يجب أن تكون غير سالبة", nameof(revisions));

        return new ServicePackage
        {
            UserId = userId,
            Name = name,
            Description = description,
            Price = price,
            DeliveryDays = deliveryDays,
            Revisions = revisions,
            CurrencyId = currencyId,
            IsActive = true,
            IsFeatured = false
        };
    }

    public void Update(string name, string description, decimal price, int deliveryDays, int revisions)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("الاسم مطلوب", nameof(name));
        if (price < 0)
            throw new ArgumentException("السعر يجب أن يكون إيجابياً", nameof(price));
        if (deliveryDays < 1)
            throw new ArgumentException("أيام التسليم يجب أن تكون أكبر من صفر", nameof(deliveryDays));
        if (revisions < 0)
            throw new ArgumentException("المراجعات يجب أن تكون غير سالبة", nameof(revisions));

        Name = name;
        Description = description;
        Price = price;
        DeliveryDays = deliveryDays;
        Revisions = revisions;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
    }
}