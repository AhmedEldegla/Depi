namespace DEPI.Domain.Entities.Wallets;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Wallet : AuditableEntity
{
    public Guid UserId { get; private set; }
    public decimal Balance { get; private set; }
    public decimal PendingBalance { get; private set; }
    public decimal TotalEarnings { get; private set; }
    public decimal TotalSpent { get; private set; }
    public string Currency { get; private set; } = "USD";
    public bool IsActive { get; private set; } = true;

    public virtual User? User { get; private set; }
    public virtual ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public virtual ICollection<Escrow> Escrows { get; private set; } = new List<Escrow>();

    private Wallet() { }

    public static Wallet Create(Guid userId, string currency = "USD")
    {
        return new Wallet
        {
            UserId = userId,
            Balance = 0,
            PendingBalance = 0,
            TotalEarnings = 0,
            TotalSpent = 0,
            Currency = currency,
            IsActive = true
        };
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        if (Balance < amount)
            throw new InvalidOperationException("الرصيد غير كافٍ");

        Balance -= amount;
    }

    public void TransferTo(Wallet targetWallet, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        if (Balance < amount)
            throw new InvalidOperationException("الرصيد غير كافٍ");

        Balance -= amount;
        targetWallet.Balance += amount;
    }

    public void AddPendingBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        if (Balance < amount)
            throw new InvalidOperationException("الرصيد غير كافٍ");

        Balance -= amount;
        PendingBalance += amount;
    }

    public void ReleasePendingBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        if (PendingBalance < amount)
            throw new InvalidOperationException("الرصيد المعلق غير كافٍ");

        PendingBalance -= amount;
    }

    public void Earn(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        Balance += amount;
        TotalEarnings += amount;
    }

    public void Spend(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("المبلغ يجب أن يكون أكبر من صفر", nameof(amount));

        if (Balance < amount)
            throw new InvalidOperationException("الرصيد غير كافٍ");

        Balance -= amount;
        TotalSpent += amount;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public decimal GetAvailableBalance()
    {
        return Balance - PendingBalance;
    }
}