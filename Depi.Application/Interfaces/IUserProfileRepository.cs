namespace DEPI.Application.Interfaces;

using System.Threading;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;

public interface IUserProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserProfile?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserProfile>> GetAvailableAsync(CancellationToken cancellationToken = default);
    Task<(IEnumerable<UserProfile> Items, int TotalCount)> GetAvailableFreelancersAsync(string? search, int page, int pageSize, CancellationToken cancellationToken = default);
}