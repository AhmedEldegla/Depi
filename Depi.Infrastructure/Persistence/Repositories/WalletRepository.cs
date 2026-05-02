using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Wallets;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class WalletRepository : Repository<Wallet>, IWalletRepository
{
    public WalletRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Wallet?> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Include(w => w.Transactions)
            .Include(w => w.Escrows)
            .FirstOrDefaultAsync(w => w.UserId == userId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(Guid walletId)
    {
        return await _context.Transactions
            .Where(t => t.WalletId == walletId || t.FromWalletId == walletId || t.ToWalletId == walletId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid transactionId)
    {
        return await _context.Transactions.FindAsync(transactionId);
    }

    public async Task<IEnumerable<Wallet>> GetTopEarningWalletsAsync(int count)
    {
        return await _dbSet
            .OrderByDescending(w => w.TotalEarnings)
            .Take(count)
            .ToListAsync();
    }
}

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }
}