using DEPI.Domain.Entities.Guilds;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Guilds;

public interface IGuildRepository : IRepository<Guild>
{
    Task<List<Guild>> GetActiveAsync();
    Task<List<Guild>> GetBySpecializationAsync(string specialization);
    Task<List<Guild>> GetTopGuildsAsync(int count);
    Task<Guild?> GetWithMembersAsync(Guid guildId);
}

public interface IGuildMemberRepository : IRepository<GuildMember>
{
    Task<List<GuildMember>> GetByGuildIdAsync(Guid guildId);
    Task<List<GuildMember>> GetByUserIdAsync(Guid userId);
    Task<GuildMember?> GetMembershipAsync(Guid guildId, Guid userId);
    Task<bool> IsMemberAsync(Guid guildId, Guid userId);
}

public interface IGuildProjectRepository : IRepository<GuildProject>
{
    Task<List<GuildProject>> GetByGuildIdAsync(Guid guildId);
    Task<List<GuildProject>> GetByProjectIdAsync(Guid projectId);
}
