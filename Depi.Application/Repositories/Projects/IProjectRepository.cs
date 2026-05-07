using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Projects;

public interface IProjectRepository : IRepository<Project>
{
<<<<<<< HEAD
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
    Task<List<Project>> GetByOwnerIdAsync(string ownerId, CancellationToken cancellationToken = default);
    Task<List<Project>> GetActiveProjectsAsync(CancellationToken cancellationToken = default);
    Task<List<Project>> SearchProjectsAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<List<Project>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task IncrementViewsAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task IncrementProposalsAsync(Guid projectId, CancellationToken cancellationToken = default);

=======
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> GetByOwnerIdAsync(Guid ownerId);
    Task<List<Project>> GetActiveProjectsAsync();
    Task<List<Project>> SearchProjectsAsync(string searchTerm);
    Task<List<Project>> GetByCategoryAsync(Guid categoryId);
    Task IncrementViewsAsync(Guid projectId);
    Task IncrementProposalsAsync(Guid projectId);
>>>>>>> master
}

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetActiveCategoriesAsync();
    Task<Category?> GetByNameAsync(string name);
}