// Wallets/CreateWallet/CreateWalletCommand.cs
using DEPI.Application.DTOs.Wallets;
using MediatR;
namespace DEPI.Application.UseCases.Wallets.CreateWallet;
public record CreateWalletCommand(Guid UserId) : IRequest<WalletResponse>;