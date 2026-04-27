
namespace Depi.Application.DTOs.Wallets
{
    public record CreateWalletRequestDto(

        Guid UserId,
        string CurrencyCode = "USD"
    );

    public record WalletResponseDto(
        Guid Id ,
        Guid UserId,
        decimal Balance,
        decimal PendingBalance,
        decimal TotalEarnings,
        decimal TotalSpent,
        decimal AvailableBalance,
        string Currency,
        bool IsActive
    );

    public record WalletListResponseDto(
        List<WalletResponseDto> Wallets,
         int TotalCount,
         int Page,
         int PageSize,
         int TotalPages
        );


}
