using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Guilds;
using DEPI.Domain.Entities.Guilds;
using MediatR;

namespace DEPI.Application.UseCases.Guilds;

public class GuildResponse { public Guid Id { get; set; } public string Name { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string Specialization { get; set; } = string.Empty; public string ImageUrl { get; set; } = string.Empty; public GuildStatus Status { get; set; } public int MemberCount { get; set; } public int CompletedProjects { get; set; } public decimal AverageRating { get; set; } public int MaxMembers { get; set; } public bool IsAcceptingMembers { get; set; } public List<GuildMemberResponse> Members { get; set; } = new(); }
public class GuildMemberResponse { public Guid Id { get; set; } public string Role { get; set; } = string.Empty; public string Skills { get; set; } = string.Empty; public DateTime JoinedAt { get; set; } }

public record CreateGuildRequest(string Name, string Description, string Specialization, string? ImageUrl, string? Requirements, int MaxMembers, decimal MinProfileScore);
public record UpdateGuildRequest(string Name, string Description, string Specialization, bool IsAcceptingMembers, int MaxMembers, decimal MinProfileScore);

public record GetGuildsQuery(string? Specialization) : IRequest<List<GuildResponse>>;
public record GetGuildQuery(Guid GuildId) : IRequest<GuildResponse?>;
public record CreateGuildCommand(Guid LeaderId, CreateGuildRequest Request) : IRequest<GuildResponse>;
public record UpdateGuildCommand(Guid GuildId, Guid UserId, UpdateGuildRequest Request) : IRequest<GuildResponse>;
public record JoinGuildCommand(Guid GuildId, Guid UserId, string Skills) : IRequest<GuildMemberResponse>;
public record LeaveGuildCommand(Guid GuildId, Guid UserId) : IRequest;
public record GetMyGuildsQuery(Guid UserId) : IRequest<List<GuildResponse>>;

public class GetGuildsQueryHandler : IRequestHandler<GetGuildsQuery, List<GuildResponse>>
{
    private readonly IGuildRepository _repo; private readonly IMapper _mapper;
    public GetGuildsQueryHandler(IGuildRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<GuildResponse>> Handle(GetGuildsQuery r, CancellationToken ct) => _mapper.Map<List<GuildResponse>>(string.IsNullOrWhiteSpace(r.Specialization) ? await _repo.GetActiveAsync() : await _repo.GetBySpecializationAsync(r.Specialization));
}

public class GetGuildQueryHandler : IRequestHandler<GetGuildQuery, GuildResponse?>
{
    private readonly IGuildRepository _repo; private readonly IMapper _mapper;
    public GetGuildQueryHandler(IGuildRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<GuildResponse?> Handle(GetGuildQuery r, CancellationToken ct) => (await _repo.GetWithMembersAsync(r.GuildId)) is { } g ? _mapper.Map<GuildResponse>(g) : null;
}

public class CreateGuildCommandHandler : IRequestHandler<CreateGuildCommand, GuildResponse>
{
    private readonly IGuildRepository _repo; private readonly IMapper _mapper;
    public CreateGuildCommandHandler(IGuildRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<GuildResponse> Handle(CreateGuildCommand r, CancellationToken ct)
    {
        var guild = new Guild { LeaderId = r.LeaderId, Name = r.Request.Name, Description = r.Request.Description, Specialization = r.Request.Specialization, ImageUrl = r.Request.ImageUrl ?? "", Requirements = r.Request.Requirements ?? "", MaxMembers = r.Request.MaxMembers, MinProfileScore = r.Request.MinProfileScore, MemberCount = 1 };
        await _repo.AddAsync(guild, ct);
        return _mapper.Map<GuildResponse>(guild);
    }
}

public class UpdateGuildCommandHandler : IRequestHandler<UpdateGuildCommand, GuildResponse>
{
    private readonly IGuildRepository _repo; private readonly IMapper _mapper;
    public UpdateGuildCommandHandler(IGuildRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<GuildResponse> Handle(UpdateGuildCommand r, CancellationToken ct)
    {
        var guild = await _repo.GetByIdAsync(r.GuildId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Guild"));
        if (guild.LeaderId != r.UserId) throw new UnauthorizedAccessException(Errors.Forbidden());
        guild.UpdateInfo(r.Request.Name, r.Request.Description, r.Request.Specialization, r.Request.IsAcceptingMembers, r.Request.MaxMembers, r.Request.MinProfileScore);
        await _repo.UpdateAsync(guild, ct);
        return _mapper.Map<GuildResponse>(guild);
    }
}

public class JoinGuildCommandHandler : IRequestHandler<JoinGuildCommand, GuildMemberResponse>
{
    private readonly IGuildRepository _guildRepo; private readonly IGuildMemberRepository _memberRepo; private readonly IMapper _mapper;
    public JoinGuildCommandHandler(IGuildRepository guildRepo, IGuildMemberRepository memberRepo, IMapper mapper) { _guildRepo = guildRepo; _memberRepo = memberRepo; _mapper = mapper; }
    public async Task<GuildMemberResponse> Handle(JoinGuildCommand r, CancellationToken ct)
    {
        var guild = await _guildRepo.GetByIdAsync(r.GuildId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Guild"));
        if (!guild.IsAcceptingMembers) throw new InvalidOperationException("Guild is not accepting members");
        if (await _memberRepo.IsMemberAsync(r.GuildId, r.UserId)) throw new InvalidOperationException(Errors.AlreadyExists("Membership"));
        var member = new GuildMember { GuildId = r.GuildId, UserId = r.UserId, Role = "Member", Skills = r.Skills, JoinedAt = DateTime.UtcNow };
        await _memberRepo.AddAsync(member, ct);
        guild.MemberCount++; await _guildRepo.UpdateAsync(guild, ct);
        return _mapper.Map<GuildMemberResponse>(member);
    }
}

public class LeaveGuildCommandHandler : IRequestHandler<LeaveGuildCommand>
{
    private readonly IGuildMemberRepository _memberRepo; private readonly IGuildRepository _guildRepo;
    public LeaveGuildCommandHandler(IGuildMemberRepository memberRepo, IGuildRepository guildRepo) { _memberRepo = memberRepo; _guildRepo = guildRepo; }
    public async Task Handle(LeaveGuildCommand r, CancellationToken ct)
    {
        var membership = await _memberRepo.GetMembershipAsync(r.GuildId, r.UserId) ?? throw new KeyNotFoundException(Errors.NotFound("Membership"));
        membership.Leave(); await _memberRepo.UpdateAsync(membership, ct);
        var guild = await _guildRepo.GetByIdAsync(r.GuildId, ct);
        if (guild != null) { guild.MemberCount--; await _guildRepo.UpdateAsync(guild, ct); }
    }
}

public class GetMyGuildsQueryHandler : IRequestHandler<GetMyGuildsQuery, List<GuildResponse>>
{
    private readonly IGuildMemberRepository _memberRepo; private readonly IGuildRepository _guildRepo; private readonly IMapper _mapper;
    public GetMyGuildsQueryHandler(IGuildMemberRepository memberRepo, IGuildRepository guildRepo, IMapper mapper) { _memberRepo = memberRepo; _guildRepo = guildRepo; _mapper = mapper; }
    public async Task<List<GuildResponse>> Handle(GetMyGuildsQuery r, CancellationToken ct)
    {
        var memberships = await _memberRepo.GetByUserIdAsync(r.UserId);
        var guilds = new List<Guild>();
        foreach (var m in memberships) { var g = await _guildRepo.GetByIdAsync(m.GuildId, ct); if (g != null) guilds.Add(g); }
        return _mapper.Map<List<GuildResponse>>(guilds);
    }
}
