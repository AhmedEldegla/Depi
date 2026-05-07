namespace DEPI.Application.Interfaces;

using System.Threading;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;

public interface IServicePackageRepository : IRepository<ServicePackage>
{
    Task<IEnumerable<ServicePackage>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ServicePackage>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ServicePackage>> GetFeaturedAsync(CancellationToken cancellationToken = default);
}