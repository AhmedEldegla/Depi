namespace DEPI.Application.Interfaces;

using System.Threading;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Interfaces;

public interface IPortfolioItemRepository : IRepository<PortfolioItem>
{
    Task<IEnumerable<PortfolioItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PortfolioItem>> GetFeaturedAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PortfolioItem>> GetPublishedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}