using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class PortfolioItemRepository : Repository<PortfolioItem>, IPortfolioItemRepository
{
    public PortfolioItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PortfolioItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.DisplayOrder)
            .ThenByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PortfolioItem>> GetFeaturedAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.IsFeatured && p.IsPublished)
            .OrderByDescending(p => p.ViewCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PortfolioItem>> GetPublishedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.IsPublished)
            .OrderByDescending(p => p.ViewCount)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}

public class ServicePackageRepository : Repository<ServicePackage>, IServicePackageRepository
{
    public ServicePackageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ServicePackage>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Currency)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.IsFeatured)
            .ThenByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServicePackage>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Currency)
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.IsFeatured)
            .ThenBy(p => p.Price)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ServicePackage>> GetFeaturedAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Currency)
            .Where(p => p.IsFeatured && p.IsActive)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}