using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Interfaces;

namespace Depi.Application.Repositories.Projects
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetActiveCategoriesAsync();
        Task<Category?> GetByNameAsync(string name);
    }
}
