using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Projects;

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
    Task<List<Project>> GetByOwnerIdAsync(string ownerId, CancellationToken cancellationToken = default);
    Task<List<Project>> GetActiveProjectsAsync(CancellationToken cancellationToken = default);
    Task<List<Project>> SearchProjectsAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<List<Project>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task IncrementViewsAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task IncrementProposalsAsync(Guid projectId, CancellationToken cancellationToken = default);
}

