using Depi.Application.DTOs.Wallets;
using DEPI.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depi.Application.UseCases.Wallets.Commands
{
    public record TransferCommand(
        Guid UserId,
        TransferRequestDto Request
    ) : IRequest<Result<TransactionResponseDto>>;
}
