

namespace Depi.Application.DTOs.Wallets
{
   public record CreateEscrowRequestDto(
       Guid ContractId,
       Guid ClientWalletId,
       Guid FreelancerWalletId,
       decimal Amount,
       string? Description = null

   );

    public record EscrowResponseDto(
       Guid Id,
       Guid ContractId,
       Guid ClientWalletId,
       Guid FreelancerWalletId,
       decimal Amount,
       decimal ReleaseAmount,
       decimal Fee,
       string Status,
       string? Description,
       DateTime? FundedAt,
       DateTime? ReleasedAt,
       DateTime? RefundedAt
    );
    public record EscrowListResponseDto(
       List<EscrowResponseDto> Escrows,
       int TotalCount,
       int Page,
       int PageSize,
       int TotalPages
    );

}
