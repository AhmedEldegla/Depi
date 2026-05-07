// Wallets/WithdrawFromWallet/WithdrawFromWalletCommand.cs
using MediatR;
namespace DEPI.Application.UseCases.Wallets.WithdrawFromWallet;
public record WithdrawFromWalletCommand(Guid UserId, decimal Amount, string? Description) : IRequest<decimal>;