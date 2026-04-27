using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Wallets.Commands
{
    public record FundEscrowCommand(
        Guid UserId,
        Guid EscrowId
    ) : IRequest<Result<EscrowResponseDto>>;

}


