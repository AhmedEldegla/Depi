// Wallets/WithdrawFromWallet/WithdrawFromWalletCommandHandler.cs
using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Wallets;
namespace DEPI.Application.UseCases.Wallets.WithdrawFromWallet;
public class WithdrawFromWalletCommandHandler : IRequestHandler<WithdrawFromWalletCommand, decimal>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    public WithdrawFromWalletCommandHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository) { _walletRepository = walletRepository; _transactionRepository = transactionRepository; }
    public async Task<decimal> Handle(WithdrawFromWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRepository.GetByUserIdAsync(request.UserId) ?? throw new KeyNotFoundException(Errors.NotFound("Wallet"));
        if (wallet.Balance < request.Amount) throw new InvalidOperationException("Insufficient balance");
        wallet.Withdraw(request.Amount);
        await _walletRepository.UpdateAsync(wallet, cancellationToken);
        var transaction = Transaction.CreateWithdrawal(wallet.Id, request.Amount, 0, request.Description ?? "Withdrawal");
        await _transactionRepository.AddAsync(transaction, cancellationToken);
        return wallet.Balance;
    }
}