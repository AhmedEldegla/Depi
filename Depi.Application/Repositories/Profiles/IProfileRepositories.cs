using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Profiles;

public interface IFreelancerProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> GetByUserIdAsync(Guid userId);
}

public interface IFreelancerSkillRepository : IRepository<FreelancerSkill>
{
    Task<List<FreelancerSkill>> GetByFreelancerIdAsync(Guid freelancerId);
    Task<FreelancerSkill?> GetByUserAndSkillAsync(Guid userId, Guid skillId);
}

public interface IPortfolioItemRepository : IRepository<PortfolioItem>
{
    Task<List<PortfolioItem>> GetByUserIdAsync(Guid userId);
    Task<List<PortfolioItem>> GetFeaturedAsync(Guid userId);
}

public interface IServicePackageRepository : IRepository<ServicePackage>
{
    Task<List<ServicePackage>> GetByUserIdAsync(Guid userId);
    Task<List<ServicePackage>> GetActiveAsync(Guid userId);
}