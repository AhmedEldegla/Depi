
namespace Depi.Application.DTOs.Wallets
{

    //Request
    public record WithdrawRequestDto(
        Guid WalletId,
        decimal Amount,
        string? Description = null
        );


    public record DepositRequestDto(
        Guid WalletId,
        decimal Amount,
        string? PaymentMethod = null,
        string? Description = null
        );

    public record TransferRequestDto(
        Guid FromWalletId,
        Guid ToWalletId,
        decimal Amount,
        string? Description = null
        );

    //Response
    public record TransactionResponseDto(
        Guid Id,
        Guid WalletId,
        Guid? FromWalletId,
        Guid? ToWalletId,
        Guid? RelatedEntityId,
        string RelatedEntityType,
        string Type,
        decimal Amount,
        decimal Fee,
        decimal NetAmount,
        string Currency,
        string Status,
        string? Description,
        string? PaymentMethod,
        string? ExternalTransactionId,
        DateTime? CompletedAt,
        DateTime CreatedAt
    );

    public record TransactionListResponseDto(
        List<TransactionResponseDto> Transactions,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages
    );




}
