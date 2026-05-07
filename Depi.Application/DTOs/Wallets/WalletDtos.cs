using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Wallets;

public class WalletResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public decimal PendingBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalSpent { get; set; }
    public string Currency { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class TransactionResponse
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public Guid? FromWalletId { get; set; }
    public Guid? ToWalletId { get; set; }
    public Guid? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal NetAmount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public TransactionStatus Status { get; set; }
    public string? Description { get; set; }
    public string? PaymentMethod { get; set; }
    public string? ExternalTransactionId { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record DepositRequest(
    decimal Amount,
    string? PaymentMethod,
    string? Description
);

public record WithdrawRequest(
    decimal Amount,
    string? Description
);

public record TransferRequest(
    Guid ToUserId,
    decimal Amount,
    string? Description
);

public record EscrowFundRequest(
    Guid ContractId,
    decimal Amount,
    string? Description
);

public class WalletSummaryResponse
{
    public decimal Balance { get; set; }
    public decimal PendingBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalSpent { get; set; }
    public int TotalTransactions { get; set; }
    public int PendingTransactions { get; set; }
}

public class WalletWithEscrowsDto
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public decimal PendingBalance { get; set; }
    public string Currency { get; set; } = string.Empty;
    public List<EscrowDto> ActiveEscrows { get; set; } = new();
    public List<TransactionResponse> RecentTransactions { get; set; } = new();
}