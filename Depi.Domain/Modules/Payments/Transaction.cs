namespace DEPI.Domain.Entities.Payments;

using Depi.Domain.Modules.Payments.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Common.Events;

public class Transaction : AuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Guid WalletId { get;set; }
    public Guid? RelatedProjectId { get;set; }
    public Guid? RelatedContractId { get;set; }
    public Guid? RelatedEscrowId { get;set; }
    public string TransactionRef { get;set; } = string.Empty;
    public TransactionType Type { get;set; }
    public TransactionStatus Status { get;set; }
    public decimal Amount { get;set; }
    public decimal Fee { get;set; }
    public decimal NetAmount => Amount - Fee;
    public string Currency { get;set; } = "USD";
    public string Description { get;set; } = string.Empty;
    public string? PaymentMethod { get;set; }
    public string? PaymentGatewayRef { get;set; }
    public string? FailureReason { get;set; }
    public DateTime? ProcessedAt { get;set; }
    public DateTime? CompletedAt { get;set; }

    public virtual Wallet Wallet { get;set; }
    public Projects.Project? RelatedProject { get;set; }
    public Projects.Contract? RelatedContract { get;set; }
    public virtual Escrow? RelatedEscrow { get;set; }

    private Transaction() { }

    public static Transaction CreateDeposit(
        Wallet wallet,
        decimal amount,
        string? paymentMethod = null,
        string? gatewayRef = null,
        string description = "Deposit")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (amount < 10)
            throw new ArgumentException("Minimum deposit is $10", nameof(amount));

        var transaction = new Transaction
        {
            WalletId = wallet.Id,
            TransactionRef = GenerateTransactionRef(TransactionType.Deposit),
            Type = TransactionType.Deposit,
            Status = TransactionStatus.Pending,
            Amount = amount,
            Fee = 0,
            Currency = wallet.Currency,
            Description = description,
            PaymentMethod = paymentMethod,
            PaymentGatewayRef = gatewayRef
        };

        transaction.RaiseDomainEvent(new TransactionCreatedEvent(transaction.Id, wallet.UserId, amount, TransactionType.Deposit));
        return transaction;
    }

    public static Transaction CreateWithdrawal(
        Wallet wallet,
        decimal amount,
        string? paymentMethod = null,
        string description = "Withdrawal")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        if (amount < 10)
            throw new ArgumentException("Minimum withdrawal is $10", nameof(amount));

        if (wallet.AvailableBalance < amount)
            throw new InvalidOperationException("Insufficient balance");

        var transaction = new Transaction
        {
            WalletId = wallet.Id,
            TransactionRef = GenerateTransactionRef(TransactionType.Withdrawal),
            Type = TransactionType.Withdrawal,
            Status = TransactionStatus.Pending,
            Amount = amount,
            Fee = CalculateWithdrawalFee(amount),
            Currency = wallet.Currency,
            Description = description,
            PaymentMethod = paymentMethod
        };

        transaction.RaiseDomainEvent(new TransactionCreatedEvent(transaction.Id, wallet.UserId, amount, TransactionType.Withdrawal));
        return transaction;
    }

    public static Transaction CreatePayment(
        Wallet wallet,
        decimal amount,
        Guid projectId,
        Guid contractId,
        string description = "Project Payment")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        var transaction = new Transaction
        {
            WalletId = wallet.Id,
            RelatedProjectId = projectId,
            RelatedContractId = contractId,
            TransactionRef = GenerateTransactionRef(TransactionType.Payment),
            Type = TransactionType.Payment,
            Status = TransactionStatus.Pending,
            Amount = amount,
            Fee = CalculatePaymentFee(amount),
            Currency = wallet.Currency,
            Description = description
        };

        transaction.RaiseDomainEvent(new TransactionCreatedEvent(transaction.Id, wallet.UserId, amount, TransactionType.Payment));
        return transaction;
    }

    public static Transaction CreateRefund(
        Wallet wallet,
        decimal amount,
        Guid projectId,
        string description = "Refund")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        var transaction = new Transaction
        {
            WalletId = wallet.Id,
            RelatedProjectId = projectId,
            TransactionRef = GenerateTransactionRef(TransactionType.Refund),
            Type = TransactionType.Refund,
            Status = TransactionStatus.Pending,
            Amount = amount,
            Fee = 0,
            Currency = wallet.Currency,
            Description = description
        };

        transaction.RaiseDomainEvent(new TransactionCreatedEvent(transaction.Id, wallet.UserId, amount, TransactionType.Refund));
        return transaction;
    }

    public static Transaction CreateBonus(
        Wallet wallet,
        decimal amount,
        string description = "Bonus")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive", nameof(amount));

        var transaction = new Transaction
        {
            WalletId = wallet.Id,
            TransactionRef = GenerateTransactionRef(TransactionType.Bonus),
            Type = TransactionType.Bonus,
            Status = TransactionStatus.Completed,
            Amount = amount,
            Fee = 0,
            Currency = wallet.Currency,
            Description = description,
            CompletedAt = DateTime.UtcNow
        };

        transaction.RaiseDomainEvent(new TransactionCreatedEvent(transaction.Id, wallet.UserId, amount, TransactionType.Bonus));
        return transaction;
    }

    public void MarkAsProcessing()
    {
        if (Status != TransactionStatus.Pending)
            throw new InvalidOperationException("Only pending transactions can be marked as processing");

        Status = TransactionStatus.Processing;
        ProcessedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted(string? gatewayRef = null)
    {
        if (Status != TransactionStatus.Pending && Status != TransactionStatus.Processing)
            throw new InvalidOperationException("Only pending or processing transactions can be completed");

        Status = TransactionStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        
        if (!string.IsNullOrEmpty(gatewayRef))
            PaymentGatewayRef = gatewayRef;

        RaiseDomainEvent(new TransactionCompletedEvent(Id, WalletId, Amount, Type));
    }

    public void MarkAsFailed(string reason)
    {
        if (Status == TransactionStatus.Completed)
            throw new InvalidOperationException("Cannot fail completed transaction");

        Status = TransactionStatus.Failed;
        FailureReason = reason;
        CompletedAt = DateTime.UtcNow;

        RaiseDomainEvent(new TransactionFailedEvent(Id, WalletId, Amount, Type, reason));
    }

    public void Cancel(string reason)
    {
        if (Status == TransactionStatus.Completed)
            throw new InvalidOperationException("Cannot cancel completed transaction");

        Status = TransactionStatus.Cancelled;
        FailureReason = reason;
        CompletedAt = DateTime.UtcNow;
    }

    private static string GenerateTransactionRef(TransactionType type)
    {
        var prefix = type switch
        {
            TransactionType.Deposit => "DEP",
            TransactionType.Withdrawal => "WDR",
            TransactionType.Payment => "PAY",
            TransactionType.Refund => "REF",
            TransactionType.Fee => "FEE",
            TransactionType.Bonus => "BNO",
            _ => "TRX"
        };

        return $"{prefix}-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
    }

    private static decimal CalculateWithdrawalFee(decimal amount)
    {
        return Math.Round(amount * 0.01m, 2);
    }

    private static decimal CalculatePaymentFee(decimal amount)
    {
        return Math.Round(amount * 0.05m, 2);
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

public class TransactionCreatedEvent : DomainEventBase
{
    public Guid TransactionId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }
    public TransactionType Type { get; }

    public override string EventType => nameof(TransactionCreatedEvent);

    public TransactionCreatedEvent(Guid transactionId, Guid userId, decimal amount, TransactionType type)
    {
        TransactionId = transactionId;
        UserId = userId;
        Amount = amount;
        Type = type;
    }
}

public class TransactionCompletedEvent : DomainEventBase
{
    public Guid TransactionId { get; }
    public Guid WalletId { get; }
    public decimal Amount { get; }
    public TransactionType Type { get; }

    public override string EventType => nameof(TransactionCompletedEvent);

    public TransactionCompletedEvent(Guid transactionId, Guid walletId, decimal amount, TransactionType type)
    {
        TransactionId = transactionId;
        WalletId = walletId;
        Amount = amount;
        Type = type;
    }
}

public class TransactionFailedEvent : DomainEventBase
{
    public Guid TransactionId { get; }
    public Guid WalletId { get; }
    public decimal Amount { get; }
    public TransactionType Type { get; }
    public string Reason { get; }

    public override string EventType => nameof(TransactionFailedEvent);

    public TransactionFailedEvent(Guid transactionId, Guid walletId, decimal amount, TransactionType type, string reason)
    {
        TransactionId = transactionId;
        WalletId = walletId;
        Amount = amount;
        Type = type;
        Reason = reason;
    }
}
