using DEPI.Application.Repositories.Proposals;
using DEPI.Domain.Entities.Proposals;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ProposalRepository : Repository<Proposal>, IProposalRepository
{
    public ProposalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Proposal?> GetByProjectAndFreelancerAsync(Guid projectId, Guid freelancerId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.ProjectId == projectId && p.FreelancerId == freelancerId);
    }

    public async Task<IEnumerable<Proposal>> GetByProjectAsync(Guid projectId)
    {
        return await _dbSet
            .Where(p => p.ProjectId == projectId)
            .Include(p => p.Freelancer)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Proposal>> GetByFreelancerAsync(Guid freelancerId)
    {
        return await _dbSet
            .Where(p => p.FreelancerId == freelancerId)
            .Include(p => p.Project)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}