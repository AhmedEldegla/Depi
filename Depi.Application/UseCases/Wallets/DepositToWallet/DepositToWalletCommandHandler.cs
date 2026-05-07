// Wallets/DepositToWallet/DepositToWalletCommandHandler.cs
using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Wallets;
namespace DEPI.Application.UseCases.Wallets.DepositToWallet;
public class DepositToWalletCommandHandler : IRequestHandler<DepositToWalletCommand, decimal>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    public DepositToWalletCommandHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository) { _walletRepository = walletRepository; _transactionRepository = transactionRepository; }
    public async Task<decimal> Handle(DepositToWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId) ?? throw new KeyNotFoundException(Errors.NotFound("Wallet"));
        wallet.Deposit(request.Amount);
        await _walletRepository.UpdateAsync(wallet, cancellationToken);
        var transaction = Transaction.CreateDeposit(wallet.Id, request.Amount, 0, request.Description ?? "Deposit");
        await _transactionRepository.AddAsync(transaction, cancellationToken);
        return wallet.Balance;
    }
}