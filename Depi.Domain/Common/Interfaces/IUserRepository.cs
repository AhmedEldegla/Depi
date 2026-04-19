namespace DEPI.Domain.Common.Interfaces;

using Depi.Domain.Modules.Identity.Enums;
using DEPI.Domain.Entities.Identity;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailWithRolesAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetByTypeAsync(UserType userType, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetActiveUsersAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<int> GetActiveUsersCountAsync(CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> PhoneNumberExistsAsync(string phoneNumber, CancellationToken cancellationToken = default);
    Task AddRoleToUserAsync(Guid userId, Guid roleId, Guid assignedBy, CancellationToken cancellationToken = default);
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default);
}
