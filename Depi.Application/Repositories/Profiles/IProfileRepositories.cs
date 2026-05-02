using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Profiles;

public interface IFreelancerProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> GetByUserIdAsync(string userId);
}

public interface IFreelancerSkillRepository : IRepository<FreelancerSkill>
{
    Task<List<FreelancerSkill>> GetByFreelancerIdAsync(string freelancerId);
    Task<FreelancerSkill?> GetByUserAndSkillAsync(string userId, Guid skillId);
}

public interface IPortfolioItemRepository : IRepository<PortfolioItem>
{
    Task<List<PortfolioItem>> GetByUserIdAsync(string userId);
    Task<List<PortfolioItem>> GetFeaturedAsync(string userId);
}

public interface IServicePackageRepository : IRepository<ServicePackage>
{
    Task<List<ServicePackage>> GetByUserIdAsync(string userId);
    Task<List<ServicePackage>> GetActiveAsync(string userId);
}