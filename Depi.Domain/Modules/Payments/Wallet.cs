namespace DEPI.Domain.Entities.Payments;

using Depi.Domain.Modules.Payments.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;
using DEPI.Domain.Entities.Identity;

public class Wallet : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid UserId { get;set; }
    public string Currency { get;set; } = "USD";
    public decimal Balance { get;set; }
    public decimal PendingBalance { get;set; }
    public decimal AvailableBalance => Balance - PendingBalance;
    public WalletStatus Status { get;set; }
    public string? BankName { get;set; }
    public string? BankAccountNumber { get;set; }
    public string? BankRoutingNumber { get;set; }
    public string? PayPalEmail { get;set; }

    public User? User { get;set; }
    public ICollection<Transaction> Transactions { get;set; } = new HashSet<Transaction>();
    public ICollection<Escrow> Escrows { get;set; } = new HashSet<Escrow>();

    private Wallet() { }

    public static Wallet Create(Guid userId, string currency = "USD")
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID is required", nameof(userId));

        var wallet = new Wallet
        {
            UserId = userId,
            Currency = currency.ToUpperInvariant(),
            Balance = 0,
            PendingBalance = 0,
            Status = WalletStatus.Active
        };

        wallet.RaiseDomainEvent(new WalletCreatedEvent(wallet.Id, userId));
        return wallet;
    }

    public void Deposit(decimal amount, string description = "Deposit")
    {
        if (Status != WalletStatus.Active)
            throw new InvalidOperationException("Cannot deposit to inactive wallet");

        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive", nameof(amount));

        if (amount < 10)
            throw new ArgumentException("Minimum deposit is $10", nameof(amount));

        Balance += amount;
        RaiseDomainEvent(new FundsDepositedEvent(Id, UserId, amount));
    }

    public void Withdraw(decimal amount, string description = "Withdrawal")
    {
        if (Status != WalletStatus.Active)
            throw new InvalidOperationException("Cannot withdraw from inactive wallet");

        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive", nameof(amount));

        if (amount < 10)
            throw new ArgumentException("Minimum withdrawal is $10", nameof(amount));

        if (AvailableBalance < amount)
            throw new InvalidOperationException("Insufficient available balance");

        Balance -= amount;
        RaiseDomainEvent(new WithdrawalRequestedEvent(Id, UserId, amount));
    }

    public void AddToPending(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (Balance < amount)
            throw new InvalidOperationException("Insufficient balance for pending");

        PendingBalance += amount;
    }

    public void RemoveFromPending(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (PendingBalance < amount)
            throw new InvalidOperationException("Pending balance less than amount");

        PendingBalance -= amount;
    }

    public void CreditBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        Balance += amount;
        RaiseDomainEvent(new WalletCreditedEvent(Id, UserId, amount));
    }

    public void DebitBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (Balance < amount)
            throw new InvalidOperationException("Insufficient balance");

        Balance -= amount;
        RaiseDomainEvent(new WalletDebitedEvent(Id, UserId, amount));
    }

    public void Suspend(string reason)
    {
        if (Status == WalletStatus.Closed)
            throw new InvalidOperationException("Cannot suspend closed wallet");

        if (Status == WalletStatus.Suspended)
            throw new InvalidOperationException("Wallet is already suspended");

        Status = WalletStatus.Suspended;
        RaiseDomainEvent(new WalletSuspendedEvent(Id, UserId, reason));
    }

    public void Activate()
    {
        if (Status == WalletStatus.Closed)
            throw new InvalidOperationException("Cannot activate closed wallet");

        Status = WalletStatus.Active;
        RaiseDomainEvent(new WalletActivatedEvent(Id, UserId));
    }

    public void Close()
    {
        if (Balance > 0)
            throw new InvalidOperationException("Cannot close wallet with balance. Withdraw all funds first.");

        Status = WalletStatus.Closed;
        RaiseDomainEvent(new WalletClosedEvent(Id, UserId));
    }

    public void UpdateBankDetails(string? bankName, string? accountNumber, string? routingNumber)
    {
        if (!string.IsNullOrEmpty(bankName))
            BankName = bankName.Trim();
        
        if (!string.IsNullOrEmpty(accountNumber))
        {
            if (accountNumber.Length < 8 || accountNumber.Length > 20)
                throw new ArgumentException("Invalid account number length", nameof(accountNumber));
            BankAccountNumber = accountNumber;
        }
        
        if (!string.IsNullOrEmpty(routingNumber))
        {
            if (routingNumber.Length != 9)
                throw new ArgumentException("Routing number must be 9 digits", nameof(routingNumber));
            BankRoutingNumber = routingNumber;
        }
    }

    public void UpdatePayPalEmail(string? email)
    {
        if (string.IsNullOrEmpty(email))
        {
            PayPalEmail = null;
            return;
        }

        if (!email.Contains('@') || !email.Contains('.'))
            throw new ArgumentException("Invalid PayPal email", nameof(email));

        PayPalEmail = email.ToLowerInvariant().Trim();
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

public class WalletCreatedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }

    public override string EventType => nameof(WalletCreatedEvent);

    public WalletCreatedEvent(Guid walletId, Guid userId)
    {
        WalletId = walletId;
        UserId = userId;
    }
}

public class FundsDepositedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public override string EventType => nameof(FundsDepositedEvent);

    public FundsDepositedEvent(Guid walletId, Guid userId, decimal amount)
    {
        WalletId = walletId;
        UserId = userId;
        Amount = amount;
    }
}

public class WithdrawalRequestedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public override string EventType => nameof(WithdrawalRequestedEvent);

    public WithdrawalRequestedEvent(Guid walletId, Guid userId, decimal amount)
    {
        WalletId = walletId;
        UserId = userId;
        Amount = amount;
    }
}

public class WalletCreditedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public override string EventType => nameof(WalletCreditedEvent);

    public WalletCreditedEvent(Guid walletId, Guid userId, decimal amount)
    {
        WalletId = walletId;
        UserId = userId;
        Amount = amount;
    }
}

public class WalletDebitedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public override string EventType => nameof(WalletDebitedEvent);

    public WalletDebitedEvent(Guid walletId, Guid userId, decimal amount)
    {
        WalletId = walletId;
        UserId = userId;
        Amount = amount;
    }
}

public class WalletSuspendedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }
    public string Reason { get; }

    public override string EventType => nameof(WalletSuspendedEvent);

    public WalletSuspendedEvent(Guid walletId, Guid userId, string reason)
    {
        WalletId = walletId;
        UserId = userId;
        Reason = reason;
    }
}

public class WalletActivatedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }

    public override string EventType => nameof(WalletActivatedEvent);

    public WalletActivatedEvent(Guid walletId, Guid userId)
    {
        WalletId = walletId;
        UserId = userId;
    }
}

public class WalletClosedEvent : DomainEventBase
{
    public Guid WalletId { get; }
    public Guid UserId { get; }

    public override string EventType => nameof(WalletClosedEvent);

    public WalletClosedEvent(Guid walletId, Guid userId)
    {
        WalletId = walletId;
        UserId = userId;
    }
}
