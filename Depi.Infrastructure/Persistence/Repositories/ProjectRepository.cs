using DEPI.Application.Repositories.Projects;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Project>> GetAllProjectsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Category)
            .Include(p => p.Owner)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Project>> GetByOwnerIdAsync(string ownerId, CancellationToken cancellationToken = default)
    {
        var ownerGuid = Guid.TryParse(ownerId, out var guid) ? guid : Guid.Empty;
        return await _dbSet
            .Where(p => p.OwnerId == ownerGuid)
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Project>> GetActiveProjectsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Status == ProjectStatus.Open || p.Status == ProjectStatus.InProgress)
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Project>> SearchProjectsAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Title.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Project>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync(cancellationToken);
    }

    public async Task IncrementViewsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await _dbSet.FindAsync(new object[] { projectId }, cancellationToken);
        if (project != null)
        {
            project.IncrementViews();
        }
    }

    public async Task IncrementProposalsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await _dbSet.FindAsync(new object[] { projectId }, cancellationToken);
        if (project != null)
        {
            project.IncrementProposals();
        }
    }

    public async Task<Project?> GetByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.OwnerId == ownerId, cancellationToken);
    }

    public async Task<Project?> GetWithProposalsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Proposals)
            .ThenInclude(p => p.Freelancer)
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetByStatusAsync(ProjectStatus status, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Status == status)
            .Include(p => p.Owner)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Project>> SearchAsync(string searchTerm, int skip, int take, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Title.Contains(searchTerm) || p.Description.Contains(searchTerm))
            .OrderByDescending(p => p.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetCountByStatusAsync(ProjectStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsQueryable();

        if (status.HasValue)
            query = query.Where(p => p.Status == status.Value);

        return await query.CountAsync(cancellationToken);
    }
}