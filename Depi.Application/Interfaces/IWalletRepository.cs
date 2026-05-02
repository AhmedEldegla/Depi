using DEPI.Domain.Entities.Wallets;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface IWalletRepository : IRepository<Wallet>
{
    Task<Wallet?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid walletId);
    Task<Transaction?> GetTransactionByIdAsync(Guid transactionId);
}

public interface ITransactionRepository : IRepository<Transaction>
{
}