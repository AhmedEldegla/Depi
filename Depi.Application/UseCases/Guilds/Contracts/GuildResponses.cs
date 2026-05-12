using DEPI.Domain.Entities.Guilds;

namespace DEPI.Application.UseCases.Guilds.Contracts;

public class GuildResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public GuildStatus Status { get; set; }
    public int MemberCount { get; set; }
    public int CompletedProjects { get; set; }
    public decimal AverageRating { get; set; }
    public int MaxMembers { get; set; }
    public bool IsAcceptingMembers { get; set; }
    public List<GuildMemberResponse> Members { get; set; } = new();
}

public class GuildMemberResponse
{
    public Guid Id { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }
}