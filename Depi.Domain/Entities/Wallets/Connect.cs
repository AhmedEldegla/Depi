using System;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

namespace DEPI.Domain.Entities.Wallets;

public class Connect : Entity
{
    public int Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsBonus { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public virtual ICollection<ConnectPurchase> Purchases { get; set; } = new List<ConnectPurchase>();
}

public class ConnectPurchase : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public Guid ConnectId { get; set; }
    public int Quantity { get; set; }
    public int FreeConnects { get; set; }
    public int TotalConnects { get; set; }
    public decimal AmountPaid { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentMethod { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Completed;
    public virtual User User { get; set; } = null!;
    public virtual Connect Connect { get; set; } = null!;
}

public enum PurchaseStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}

public class ConnectUsage : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public Guid? ProjectId { get; set; }
    public Guid? JobId { get; set; }
    public int ConnectsUsed { get; set; }
    public string UsageType { get; set; } = string.Empty;
    public bool IsRefunded { get; set; }

    public virtual User User { get; set; } = null!;
}

public class FreelancerSubscription : AuditableEntity
{
    public string UserId { get; set; } = string.Empty;
    public SubscriptionTier Tier { get; set; } = SubscriptionTier.Basic;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;
    public string PaymentMethod { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public DateTime? CancelledAt { get; set; }
    public DateTime? RenewalDate { get; set; }

    public virtual User User { get; set; } = null!;
}

public enum SubscriptionTier
{
    Basic,
    Plus,
    Premium,
    Enterprise
}

public enum SubscriptionStatus
{
    Active,
    Expired,
    Cancelled,
    Pending,
    Trial
}