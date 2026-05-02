using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetWithRolesAsync(Guid id);
    Task<User?> GetByRefreshTokenAsync(string token);
}