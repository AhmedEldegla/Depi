using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Projects;

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetByIdAsync(Guid id);
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> GetByOwnerIdAsync(string ownerId);
    Task<List<Project>> GetActiveProjectsAsync();
    Task<List<Project>> SearchProjectsAsync(string searchTerm);
    Task<List<Project>> GetByCategoryAsync(Guid categoryId);
    Task IncrementViewsAsync(Guid projectId);
    Task IncrementProposalsAsync(Guid projectId);
}

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetActiveCategoriesAsync();
    Task<Category?> GetByNameAsync(string name);
}