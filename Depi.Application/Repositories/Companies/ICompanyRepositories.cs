using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Companies;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Companies;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByOwnerIdAsync(string ownerId);
    Task<List<Company>> GetVerifiedCompaniesAsync();
    Task<List<Company>> GetTopCompaniesAsync(int count);
    Task<List<Company>> SearchCompaniesAsync(string searchTerm);
}

public interface ICompanyMemberRepository : IRepository<CompanyMember>
{
    Task<List<CompanyMember>> GetByCompanyIdAsync(Guid companyId);
    Task<List<CompanyMember>> GetByUserIdAsync(string userId);
    Task<CompanyMember?> GetMemberAsync(Guid companyId, string userId);
    Task<bool> IsMemberAsync(Guid companyId, string userId);
}

public interface ICompanyProjectRepository : IRepository<CompanyProject>
{
    Task<List<CompanyProject>> GetByCompanyIdAsync(Guid companyId);
    Task<List<CompanyProject>> GetByProjectIdAsync(Guid projectId);
}

public interface ICompanyFollowerRepository : IRepository<CompanyFollower>
{
    Task<List<CompanyFollower>> GetByCompanyIdAsync(Guid companyId);
    Task<List<CompanyFollower>> GetByUserIdAsync(string userId);
    Task<bool> IsFollowingAsync(Guid companyId, string userId);
}

public interface ICompanyReviewRepository : IRepository<CompanyReview>
{
    Task<List<CompanyReview>> GetByCompanyIdAsync(Guid companyId);
    Task<List<CompanyReview>> GetVerifiedReviewsAsync(Guid companyId);
    Task<decimal> GetAverageRatingAsync(Guid companyId);
}