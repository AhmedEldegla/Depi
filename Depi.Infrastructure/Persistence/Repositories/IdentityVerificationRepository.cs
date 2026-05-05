namespace DEPI.Infrastructure.Persistence.Repositories;

using DEPI.Application.Repositories.Verifications;
using DEPI.Domain.Entities.Verifications;
using Microsoft.EntityFrameworkCore;

public class IdentityVerificationRepository : IIdentityVerificationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<IdentityVerification> _dbSet;

    public IdentityVerificationRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<IdentityVerification>();
    }

    public async Task<IdentityVerification?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(v => v.User)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IdentityVerification?> GetPendingByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(v => v.UserId == userId && v.Status == Domain.Enums.VerificationStatus.Pending);
    }

    public async Task<List<IdentityVerification>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(v => v.UserId == userId)
            .OrderByDescending(v => v.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<IdentityVerification>> GetPendingVerificationsAsync()
    {
        return await _dbSet
            .Include(v => v.User)
            .Where(v => v.Status == Domain.Enums.VerificationStatus.Pending)
            .OrderBy(v => v.CreatedAt)
            .ToListAsync();
    }

    public async Task<IdentityVerification> AddAsync(IdentityVerification verification)
    {
        await _dbSet.AddAsync(verification);
        await _context.SaveChangesAsync();
        return verification;
    }

    public async Task UpdateAsync(IdentityVerification verification)
    {
        _dbSet.Update(verification);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> HasExistingVerificationAsync(Guid userId)
    {
        return await _dbSet.AnyAsync(v => v.UserId == userId && v.Status == Domain.Enums.VerificationStatus.Pending);
    }
}
