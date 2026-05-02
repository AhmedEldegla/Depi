using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Media;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Shared;
using DEPI.Domain.Enums;
using DEPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Country?> GetByIso2Async(string iso2, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Iso2 == iso2.ToUpperInvariant(), cancellationToken);
    }

    public async Task<IEnumerable<Country>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }
}

public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
{
    public CurrencyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Currency?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Code == code.ToUpperInvariant(), cancellationToken);
    }

    public async Task<Currency?> GetDefaultAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.IsDefault, cancellationToken);
    }

    public async Task<IEnumerable<Currency>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.IsActive)
            .OrderBy(c => c.Code)
            .ToListAsync(cancellationToken);
    }
}

public class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Skill?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.Name == name || s.NameEn == name, cancellationToken);
    }

    public async Task<List<Skill>> GetPopularAsync(int count, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.IsActive)
            .OrderByDescending(s => s.DisplayOrder)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Skill>> GetAllAsync(bool? isActive = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(s => s.IsActive == isActive.Value);
        return await query.OrderBy(s => s.DisplayOrder).ToListAsync(cancellationToken);
    }

    public async Task<Skill?> GetByNameEnAsync(string nameEn, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.NameEn == nameEn, cancellationToken);
    }

    public async Task<IEnumerable<Skill>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Skill>> GetVerifiedAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(s => s.IsVerified)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameEnAsync(string nameEn, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(s => s.NameEn == nameEn, cancellationToken);
    }
}

public class MediaFileRepository : Repository<MediaFile>, IMediaFileRepository
{
    public MediaFileRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<MediaFile?> GetByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.OwnerId == ownerId, cancellationToken);
    }

    public async Task<MediaFile?> GetByTypeAsync(Guid ownerId, MediaType type, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.OwnerId == ownerId && m.Type == type, cancellationToken);
    }

    public async Task<MediaFile?> GetAvatarAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(m => m.OwnerId == ownerId && m.Type == MediaType.Avatar && m.IsActive, cancellationToken);
    }

    public async Task<IEnumerable<MediaFile>> GetFilesAsync(Guid? ownerId, MediaType? type = null, bool? isActive = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (ownerId.HasValue)
            query = query.Where(m => m.OwnerId == ownerId.Value);

        if (type.HasValue)
            query = query.Where(m => m.Type == type.Value);

        if (isActive.HasValue)
            query = query.Where(m => m.IsActive == isActive.Value);

        return await query.OrderByDescending(m => m.CreatedAt).ToListAsync(cancellationToken);
    }
}