namespace DEPI.Domain.Entities.Wallets;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Enums;

public class Transaction : AuditableEntity
{
    public Guid WalletId { get; private set; }
    public Guid? FromWalletId { get; private set; }
    public Guid? ToWalletId { get; private set; }
    public Guid? RelatedEntityId { get; private set; }
    public string RelatedEntityType { get; private set; } = string.Empty;
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public decimal Fee { get; private set; }
    public decimal NetAmount { get; private set; }
    public string Currency { get; private set; } = "USD";
    public TransactionStatus Status { get; private set; }
    public string? Description { get; private set; }
    public string? PaymentMethod { get; private set; }
    public string? ExternalTransactionId { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public virtual Wallet? Wallet { get; private set; }
    public virtual Wallet? FromWallet { get; private set; }
    public virtual Wallet? ToWallet { get; private set; }

    private Transaction() { }

    public static Transaction CreateDeposit(
        Guid walletId,
        decimal amount,
        decimal fee = 0,
        string? description = null,
        string? paymentMethod = null)
    {
        return new Transaction
        {
            WalletId = walletId,
            Type = TransactionType.Deposit,
            Amount = amount,
            Fee = fee,
            NetAmount = amount - fee,
            Currency = "USD",
            Status = TransactionStatus.Pending,
            Description = description,
            PaymentMethod = paymentMethod
        };
    }

    public static Transaction CreateWithdrawal(
        Guid walletId,
        decimal amount,
        decimal fee = 0,
        string? description = null)
    {
        return new Transaction
        {
            WalletId = walletId,
            Type = TransactionType.Withdrawal,
            Amount = amount,
            Fee = fee,
            NetAmount = amount - fee,
            Currency = "USD",
            Status = TransactionStatus.Pending,
            Description = description
        };
    }

    public static Transaction CreateTransfer(
        Guid fromWalletId,
        Guid toWalletId,
        decimal amount,
        decimal fee = 0,
        string? description = null)
    {
        return new Transaction
        {
            FromWalletId = fromWalletId,
            ToWalletId = toWalletId,
            Type = TransactionType.Transfer,
            Amount = amount,
            Fee = fee,
            NetAmount = amount - fee,
            Currency = "USD",
            Status = TransactionStatus.Pending,
            Description = description
        };
    }

    public static Transaction CreateEscrow(
        Guid walletId,
        Guid contractId,
        decimal amount,
        string? description = null)
    {
        return new Transaction
        {
            WalletId = walletId,
            RelatedEntityId = contractId,
            RelatedEntityType = "Contract",
            Type = TransactionType.Escrow,
            Amount = amount,
            Fee = 0,
            NetAmount = amount,
            Currency = "USD",
            Status = TransactionStatus.Pending,
            Description = description ?? "Escrow for contract"
        };
    }

    public static Transaction CreateCommission(
        Guid walletId,
        decimal amount,
        string? description = null)
    {
        return new Transaction
        {
            WalletId = walletId,
            Type = TransactionType.Commission,
            Amount = amount,
            Fee = 0,
            NetAmount = -amount,
            Currency = "USD",
            Status = TransactionStatus.Completed,
            Description = description ?? "Platform commission",
            CompletedAt = DateTime.UtcNow
        };
    }

    public void MarkAsCompleted()
    {
        Status = TransactionStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = TransactionStatus.Failed;
    }

    public void MarkAsCancelled()
    {
        Status = TransactionStatus.Cancelled;
    }

    public void SetExternalTransactionId(string externalId)
    {
        ExternalTransactionId = externalId;
    }
}