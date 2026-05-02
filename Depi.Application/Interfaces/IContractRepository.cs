using DEPI.Domain.Entities.Contracts;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface IContractRepository : IRepository<Contract>
{
    Task<Contract?> GetByProjectAsync(Guid projectId);
    Task<Contract?> GetByIdAsync(Guid id, bool includeMilestones = false);
    Task<IEnumerable<Contract>> GetByClientAsync(Guid clientId);
    Task<IEnumerable<Contract>> GetByFreelancerAsync(Guid freelancerId);
}