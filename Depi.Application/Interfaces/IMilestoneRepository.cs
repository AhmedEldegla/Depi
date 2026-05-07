using DEPI.Domain.Entities.Contracts;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface IMilestoneRepository : IRepository<Milestone>
{
    Task<IEnumerable<Milestone>> GetByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default);
}