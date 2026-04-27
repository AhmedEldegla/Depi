using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Application.DTOs.Wallets
{

    //Request
    public record WithdrawRequest(
        Guid walletId,
        decimal amount,
        string? description = null
        );


    public record DepositRequest(
        Guid walletId,
        decimal amount,
        string? PaymentMethod = null,
        string? Description = null
        );

    public record TransferRequest(
        Guid fromWalletId,
        Guid toWalletId,
        decimal amount,
        string? description = null
        );

    //Response
    public record TransactionResponse(
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

    public record TransactionListResponse(
        List<TransactionResponse> Transactions,
        int TotalCount,
        int Page,
        int PageSize,
        int TotalPages
    );




}
