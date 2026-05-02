// Wallets/DepositToWallet/DepositToWalletCommand.cs
using MediatR;
namespace DEPI.Application.UseCases.Wallets.DepositToWallet;
public record DepositToWalletCommand(Guid UserId, decimal Amount, string? Description) : IRequest<decimal>;