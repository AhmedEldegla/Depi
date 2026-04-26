using DEPI.Domain.Entities.Proposals;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Proposals;

public interface IProposalRepository : IRepository<Proposal>
{
    Task<Proposal?> GetByProjectAndFreelancerAsync(Guid projectId, Guid freelancerId);
    Task<IEnumerable<Proposal>> GetByProjectAsync(Guid projectId);
    Task<IEnumerable<Proposal>> GetByFreelancerAsync(Guid freelancerId);
}