using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;

namespace Depi.Application.UseCases.Wallets.Commands
{
    public record CreateEscrowCommand(
          Guid UserId,
          CreateEscrowRequestDto Request
          ) : IRequest<Result<EscrowResponseDto>>;
}
