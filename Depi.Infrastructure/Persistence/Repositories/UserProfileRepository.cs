using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Country)
            .Include(p => p.Currency)
            .FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
    }

    public async Task<UserProfile?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        if (Guid.TryParse(userId, out var guid))
        {
            return await GetByUserIdAsync(guid, cancellationToken);
        }
        return null;
    }

    public async Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Country)
            .Include(p => p.Currency)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(p => p.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<UserProfile>> GetAvailableAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Country)
            .Include(p => p.Currency)
            .Where(p => p.IsAvailable)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserProfile>> SearchAsync(
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .Include(p => p.Country)
            .Include(p => p.Currency)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(p =>
                p.DisplayName.ToLower().Contains(searchLower) ||
                p.Title.ToLower().Contains(searchLower) ||
                p.Bio.ToLower().Contains(searchLower));
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<UserProfile> Items, int TotalCount)> GetAvailableFreelancersAsync(
        string? search,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .Include(p => p.Country)
            .Include(p => p.Currency)
            .Where(p => p.IsAvailable)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(p =>
                p.DisplayName.ToLower().Contains(searchLower) ||
                p.Title.ToLower().Contains(searchLower) ||
                p.Bio.ToLower().Contains(searchLower));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(p => p.CompletedProjects)
            .ThenBy(p => p.ResponseTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}