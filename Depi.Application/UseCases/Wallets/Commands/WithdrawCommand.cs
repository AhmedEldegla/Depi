using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;

namespace Depi.Application.UseCases.Wallets.Commands
{
    public record WithdrawCommand(
        Guid UserId,
        WithdrawRequestDto Request
    ) : IRequest<Result<TransactionResponseDto>>;
}
