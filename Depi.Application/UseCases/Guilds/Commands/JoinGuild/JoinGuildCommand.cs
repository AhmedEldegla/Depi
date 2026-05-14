using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.UseCases.Guilds.Contracts;
using DEPI.Domain.Entities.Guilds;

namespace DEPI.Application.UseCases.Guilds.Commands.JoinGuild
{
    public record JoinGuildCommand(Guid GuildId, Guid UserId, string Skills) : IRequest<GuildMemberResponse>;

    public class JoinGuildCommandHandler : IRequestHandler<JoinGuildCommand, GuildMemberResponse>
    {
        private readonly IGuildRepository _guildRepo;
        private readonly IGuildMemberRepository _memberRepo;
        private readonly IMapper _mapper;

        public JoinGuildCommandHandler(IGuildRepository guildRepo, IGuildMemberRepository memberRepo, IMapper mapper)
        {
            _guildRepo = guildRepo; _memberRepo = memberRepo; _mapper = mapper;
        }

        public async Task<GuildMemberResponse> Handle(JoinGuildCommand r, CancellationToken ct)
        {
            var guild = await _guildRepo.GetByIdAsync(r.GuildId) ?? throw new KeyNotFoundException("Guild not found");

            if (!guild.IsAcceptingMembers) throw new InvalidOperationException("Guild is not accepting members");
            if (guild.MemberCount >= guild.MaxMembers) throw new InvalidOperationException("Guild is full");
            if (await _memberRepo.IsMemberAsync(r.GuildId, r.UserId)) throw new InvalidOperationException("Already a member");

            var member = new GuildMember
            {
                GuildId = r.GuildId,
                UserId = r.UserId,
                Role = "Member",
                Skills = r.Skills,
                JoinedAt = DateTime.UtcNow,
                IsActive = true
            };
            await _memberRepo.AddAsync(member);

            guild.MemberCount++;
            await _guildRepo.UpdateAsync(guild);

            return _mapper.Map<GuildMemberResponse>(member);
        }
    }
}