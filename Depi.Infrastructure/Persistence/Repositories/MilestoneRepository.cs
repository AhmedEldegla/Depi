using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class MilestoneRepository : Repository<Milestone>, IMilestoneRepository
{
    public MilestoneRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<Milestone>> GetByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(m => m.ContractId == contractId).OrderBy(m => m.CreatedAt).ToListAsync(cancellationToken);
    }
}
