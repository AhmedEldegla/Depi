using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Wallets.Queries
{
    public record GetWalletBalanceQuery(
        Guid UserId
    ) : IRequest<Result<decimal>>;

}


