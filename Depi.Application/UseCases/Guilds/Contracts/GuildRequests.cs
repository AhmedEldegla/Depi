namespace DEPI.Application.UseCases.Guilds.Contracts;

public record CreateGuildRequest(string Name, string Description, string Specialization, string? ImageUrl, string? Requirements, int MaxMembers, decimal MinProfileScore);

public record UpdateGuildRequest(string Name, string Description, string Specialization, bool IsAcceptingMembers, int MaxMembers, decimal MinProfileScore);