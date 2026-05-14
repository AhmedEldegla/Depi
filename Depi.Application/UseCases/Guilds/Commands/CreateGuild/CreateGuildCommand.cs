using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.UseCases.Guilds.Contracts;

namespace DEPI.Application.UseCases.Guilds.Commands.CreateGuild
{
    public record CreateGuildCommand(Guid LeaderId, CreateGuildRequest Request) : IRequest<GuildResponse>;

    public class CreateGuildCommandHandler : IRequestHandler<CreateGuildCommand, GuildResponse>
    {
        private readonly IGuildRepository _guildRepo;
        private readonly IGuildMemberRepository _memberRepo;
        private readonly IMapper _mapper;

        public CreateGuildCommandHandler(IGuildRepository guildRepo, IGuildMemberRepository memberRepo, IMapper mapper)
        {
            _guildRepo = guildRepo;
            _memberRepo = memberRepo;
            _mapper = mapper;
        }

        public async Task<GuildResponse> Handle(CreateGuildCommand r, CancellationToken ct)
        {
            var guild = new DEPI.Domain.Entities.Guilds.Guild
            {
                LeaderId = r.LeaderId,
                Name = r.Request.Name,
                Description = r.Request.Description,
                Specialization = r.Request.Specialization,
                ImageUrl = r.Request.ImageUrl ?? "",
                Requirements = r.Request.Requirements ?? "",
                MaxMembers = r.Request.MaxMembers,
                MinProfileScore = r.Request.MinProfileScore,
                MemberCount = 1
            };

            await _guildRepo.AddAsync(guild);

            var leaderMember = new DEPI.Domain.Entities.Guilds.GuildMember
            {
                GuildId = guild.Id,
                UserId = r.LeaderId,
                Role = "Leader",
                JoinedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _memberRepo.AddAsync(leaderMember);

            return _mapper.Map<GuildResponse>(guild);
        }
    }
}