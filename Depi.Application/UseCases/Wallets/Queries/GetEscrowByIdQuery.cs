using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Wallets.Queries
{
    public record GetEscrowByIdQuery(
        Guid EscrowId
    ) : IRequest<Result<EscrowResponseDto>>;

}