using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Reviews;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface ISkillRepository : IRepository<Skill>
{
    Task<Skill?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<List<Skill>> GetPopularAsync(int count, CancellationToken cancellationToken = default);
    Task<IEnumerable<Skill>> GetAllAsync(bool? isActive = null, CancellationToken cancellationToken = default);
}

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
    Task<List<Project>> GetByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task<List<Project>> GetActiveProjectsAsync(CancellationToken cancellationToken = default);
    Task<List<Project>> SearchProjectsAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<List<Project>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task IncrementViewsAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task IncrementProposalsAsync(Guid projectId, CancellationToken cancellationToken = default);
}

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetByRevieweeIdAsync(Guid revieweeId, CancellationToken cancellationToken = default);
    Task<List<Review>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<List<Review>> GetByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default);
    Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<decimal> GetAverageRatingAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<int> GetReviewCountAsync(Guid userId, CancellationToken cancellationToken = default);
}