using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Application.DTOs.Wallets
{
   public record EscrowRequest(
       Guid ContractId,
       Guid ClientWalletId,
       Guid FreelancerWalletId,
       decimal Amount,
       string? Description = null

   );

    public record EscrowResponse(
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
    public record EscrowListResponse(
       List<EscrowResponse> Escrows,
       int TotalCount,
       int Page,
       int PageSize,
       int TotalPages
    );

}
