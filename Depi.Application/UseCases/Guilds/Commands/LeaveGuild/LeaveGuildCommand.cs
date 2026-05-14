using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DEPI.Application.Repositories.Guilds;

namespace DEPI.Application.UseCases.Guilds.Commands.LeaveGuild
{
    public record LeaveGuildCommand(Guid GuildId, Guid UserId) : IRequest;

    public class LeaveGuildCommandHandler : IRequestHandler<LeaveGuildCommand>
    {
        private readonly IGuildMemberRepository _memberRepo;
        private readonly IGuildRepository _guildRepo;

        public LeaveGuildCommandHandler(IGuildMemberRepository memberRepo, IGuildRepository guildRepo)
        {
            _memberRepo = memberRepo; _guildRepo = guildRepo;
        }

        public async Task Handle(LeaveGuildCommand r, CancellationToken ct)
        {
            var membership = await _memberRepo.GetMembershipAsync(r.GuildId, r.UserId) ?? throw new KeyNotFoundException("Membership not found");
            var guild = await _guildRepo.GetByIdAsync(r.GuildId) ?? throw new KeyNotFoundException("Guild not found");

            membership.Leave();
            await _memberRepo.UpdateAsync(membership);

            guild.MemberCount--;
            await _guildRepo.UpdateAsync(guild);
        }
    }
}