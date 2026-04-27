using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Application.DTOs.Wallets
{
    public record CreateWalletRequest(

        Guid UserId,
        string CurrencyCode = "USD"
    );

    public record WalletRespons(
        Guid Id ,
        Guid UserId,
        decimal Balance,
        decimal PendingBalance,
        decimal TotalEarnings,
        decimal TotalSpent,
        string Currency,
        bool IsActive
    );

    public record WalletListResponse(
        List<WalletRespons> Wallets,
         int TotalCount,
         int Page,
         int PageSize,
         int TotalPages
        );


}
