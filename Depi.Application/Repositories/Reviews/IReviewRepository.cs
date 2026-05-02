using DEPI.Domain.Entities.Reviews;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Reviews;

public interface IReviewRepository : IRepository<Review>
{
    Task<List<Review>> GetByRevieweeIdAsync(string revieweeId);
    Task<List<Review>> GetByProjectIdAsync(Guid projectId);
    Task<List<Review>> GetByContractIdAsync(Guid contractId);
    Task<Review?> GetByIdAsync(Guid id);
    Task<decimal> GetAverageRatingAsync(string userId);
    Task<int> GetReviewCountAsync(string userId);
}