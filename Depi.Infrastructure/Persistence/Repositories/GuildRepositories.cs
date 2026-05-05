using DEPI.Application.Repositories.Guilds;
using DEPI.Domain.Entities.Guilds;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class GuildRepository : Repository<Guild>, IGuildRepository
{
    public GuildRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Guild>> GetActiveAsync()
        => await _dbSet.Where(g => g.Status == GuildStatus.Active).OrderByDescending(g => g.AverageRating).ToListAsync();

    public async Task<List<Guild>> GetBySpecializationAsync(string specialization)
        => await _dbSet.Where(g => g.Specialization.Contains(specialization) && g.Status == GuildStatus.Active).ToListAsync();

    public async Task<List<Guild>> GetTopGuildsAsync(int count)
        => await _dbSet.Where(g => g.Status == GuildStatus.Active).OrderByDescending(g => g.CompletedProjects).ThenByDescending(g => g.AverageRating).Take(count).ToListAsync();

    public async Task<Guild?> GetWithMembersAsync(Guid guildId)
        => await _dbSet.Include(g => g.Members).ThenInclude(m => m.User).FirstOrDefaultAsync(g => g.Id == guildId);
}

public class GuildMemberRepository : Repository<GuildMember>, IGuildMemberRepository
{
    public GuildMemberRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<GuildMember>> GetByGuildIdAsync(Guid guildId)
        => await _dbSet.Where(m => m.GuildId == guildId && m.IsActive).ToListAsync();

    public async Task<List<GuildMember>> GetByUserIdAsync(Guid userId)
        => await _dbSet.Where(m => m.UserId == userId && m.IsActive).ToListAsync();

    public async Task<GuildMember?> GetMembershipAsync(Guid guildId, Guid userId)
        => await _dbSet.FirstOrDefaultAsync(m => m.GuildId == guildId && m.UserId == userId);

    public async Task<bool> IsMemberAsync(Guid guildId, Guid userId)
        => await _dbSet.AnyAsync(m => m.GuildId == guildId && m.UserId == userId && m.IsActive);
}

public class GuildProjectRepository : Repository<GuildProject>, IGuildProjectRepository
{
    public GuildProjectRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<GuildProject>> GetByGuildIdAsync(Guid guildId)
        => await _dbSet.Where(gp => gp.GuildId == guildId).OrderByDescending(gp => gp.CreatedAt).ToListAsync();

    public async Task<List<GuildProject>> GetByProjectIdAsync(Guid projectId)
        => await _dbSet.Where(gp => gp.ProjectId == projectId).ToListAsync();
}
