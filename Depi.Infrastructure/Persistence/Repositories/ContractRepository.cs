using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ContractRepository : Repository<Contract>, IContractRepository
{
    public ContractRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Contract?> GetByProjectAsync(Guid projectId)
    {
        return await _dbSet
            .Include(c => c.Milestones)
            .FirstOrDefaultAsync(c => c.ProjectId == projectId);
    }

    public async Task<Contract?> GetByIdAsync(Guid id, bool includeMilestones = false)
    {
        var query = _dbSet.AsQueryable();

        if (includeMilestones)
            query = query.Include(c => c.Milestones);

        return await query.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Contract>> GetByClientAsync(Guid clientId)
    {
        return await _dbSet
            .Where(c => c.ClientId == clientId)
            .Include(c => c.Project)
            .Include(c => c.Freelancer)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Contract>> GetByFreelancerAsync(Guid freelancerId)
    {
        return await _dbSet
            .Where(c => c.FreelancerId == freelancerId)
            .Include(c => c.Project)
            .Include(c => c.Client)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
}