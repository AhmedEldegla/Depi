using DEPI.Domain.Entities.Reviews;

namespace Depi.Application.Repositories.Common;

public interface IReviewRepository
{
    Task<List<Review>> GetByRevieweeIdAsync(string revieweeId, CancellationToken cancellationToken = default);
    Task<List<Review>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<List<Review>> GetByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default);
    Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<decimal> GetAverageRatingAsync(string userId, CancellationToken cancellationToken = default);
    Task<int> GetReviewCountAsync(string userId, CancellationToken cancellationToken = default);
}