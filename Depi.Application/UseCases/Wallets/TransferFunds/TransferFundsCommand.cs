// Wallets/TransferFunds/TransferFundsCommand.cs
using MediatR;
namespace DEPI.Application.UseCases.Wallets.TransferFunds;
public record TransferFundsCommand(Guid FromUserId, Guid ToUserId, decimal Amount, string? Description) : IRequest<Unit>;