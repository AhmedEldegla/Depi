// Wallets/TransferFunds/TransferFundsCommandHandler.cs
using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Wallets;
namespace DEPI.Application.UseCases.Wallets.TransferFunds;
public class TransferFundsCommandHandler : IRequestHandler<TransferFundsCommand, Unit>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    public TransferFundsCommandHandler(IWalletRepository walletRepository, ITransactionRepository transactionRepository) { _walletRepository = walletRepository; _transactionRepository = transactionRepository; }
    public async Task<Unit> Handle(TransferFundsCommand request, CancellationToken cancellationToken)
    {
        if (request.FromUserId == request.ToUserId) throw new InvalidOperationException("Cannot transfer to yourself");
        var fromWallet = await _walletRepository.GetByUserIdAsync(request.FromUserId) ?? throw new KeyNotFoundException(Errors.NotFound("Sender Wallet"));
        var toWallet = await _walletRepository.GetByUserIdAsync(request.ToUserId) ?? throw new KeyNotFoundException(Errors.NotFound("Recipient Wallet"));
        if (fromWallet.Balance < request.Amount) throw new InvalidOperationException("Insufficient balance");
        fromWallet.TransferTo(toWallet, request.Amount);
        await _walletRepository.UpdateAsync(fromWallet, cancellationToken);
        await _walletRepository.UpdateAsync(toWallet, cancellationToken);
        var transaction = Transaction.CreateTransfer(fromWallet.Id, toWallet.Id, request.Amount, 0, request.Description ?? "Transfer");
        await _transactionRepository.AddAsync(transaction, cancellationToken);
        return Unit.Value;
    }
}