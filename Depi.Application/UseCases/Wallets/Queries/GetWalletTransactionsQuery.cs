using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Wallets.Queries
{
    public record GetWalletTransactionsQuery(
        Guid UserId,
        int Page = 1,
        int PageSize = 10
    ) : IRequest<Result<TransactionListResponseDto>>;

}


