using DEPI.Application.Repositories.Companies;
using DEPI.Domain.Entities.Companies;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Company?> GetByOwnerIdAsync(string ownerId)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.OwnerId == ownerId);
    }

    public async Task<List<Company>> GetVerifiedCompaniesAsync()
    {
        return await _dbSet.Where(c => c.IsVerified).ToListAsync();
    }

    public async Task<List<Company>> GetTopCompaniesAsync(int count)
    {
        return await _dbSet.OrderByDescending(c => c.Rating).Take(count).ToListAsync();
    }

    public async Task<List<Company>> SearchCompaniesAsync(string searchTerm)
    {
        return await _dbSet.Where(c => c.Name.Contains(searchTerm)).ToListAsync();
    }
}

public class CompanyMemberRepository : Repository<CompanyMember>, ICompanyMemberRepository
{
    public CompanyMemberRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CompanyMember>> GetByCompanyIdAsync(Guid companyId)
    {
        return await _dbSet.Where(m => m.CompanyId == companyId).ToListAsync();
    }

    public async Task<List<CompanyMember>> GetByUserIdAsync(string userId)
    {
        return await _dbSet.Where(m => m.UserId == userId).ToListAsync();
    }

    public async Task<CompanyMember?> GetMemberAsync(Guid companyId, string userId)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.CompanyId == companyId && m.UserId == userId);
    }

    public async Task<bool> IsMemberAsync(Guid companyId, string userId)
    {
        return await _dbSet.AnyAsync(m => m.CompanyId == companyId && m.UserId == userId);
    }
}

public class CompanyProjectRepository : Repository<CompanyProject>, ICompanyProjectRepository
{
    public CompanyProjectRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CompanyProject>> GetByCompanyIdAsync(Guid companyId)
    {
        return await _dbSet.Where(cp => cp.CompanyId == companyId).ToListAsync();
    }

    public async Task<List<CompanyProject>> GetByProjectIdAsync(Guid projectId)
    {
        return await _dbSet.Where(cp => cp.ProjectId == projectId).ToListAsync();
    }
}

public class CompanyFollowerRepository : Repository<CompanyFollower>, ICompanyFollowerRepository
{
    public CompanyFollowerRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CompanyFollower>> GetByCompanyIdAsync(Guid companyId)
    {
        return await _dbSet.Where(f => f.CompanyId == companyId).ToListAsync();
    }

    public async Task<List<CompanyFollower>> GetByUserIdAsync(string userId)
    {
        return await _dbSet.Where(f => f.UserId == userId).ToListAsync();
    }

    public async Task<bool> IsFollowingAsync(Guid companyId, string userId)
    {
        return await _dbSet.AnyAsync(f => f.CompanyId == companyId && f.UserId == userId);
    }
}

public class CompanyReviewRepository : Repository<CompanyReview>, ICompanyReviewRepository
{
    public CompanyReviewRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CompanyReview>> GetByCompanyIdAsync(Guid companyId)
    {
        return await _dbSet.Where(r => r.CompanyId == companyId).ToListAsync();
    }

    public async Task<List<CompanyReview>> GetVerifiedReviewsAsync(Guid companyId)
    {
        return await _dbSet.Where(r => r.CompanyId == companyId && r.IsVerified).ToListAsync();
    }

    public async Task<decimal> GetAverageRatingAsync(Guid companyId)
    {
        var ratings = await _dbSet.Where(r => r.CompanyId == companyId).Select(r => (decimal)r.Rating).ToListAsync();
        return ratings.Count > 0 ? ratings.Average() : 0m;
    }
}
