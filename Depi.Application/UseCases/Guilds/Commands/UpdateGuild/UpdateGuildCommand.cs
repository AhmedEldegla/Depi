using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.UseCases.Guilds.Contracts;

namespace DEPI.Application.UseCases.Guilds.Commands.UpdateGuild
{
    public record UpdateGuildCommand(Guid GuildId, Guid UserId, UpdateGuildRequest Request) : IRequest<GuildResponse>;

    public class UpdateGuildCommandHandler : IRequestHandler<UpdateGuildCommand, GuildResponse>
    {
        private readonly IGuildRepository _repo;
        private readonly IMapper _mapper;

        public UpdateGuildCommandHandler(IGuildRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<GuildResponse> Handle(UpdateGuildCommand r, CancellationToken ct)
        {
            var guild = await _repo.GetByIdAsync(r.GuildId) ?? throw new KeyNotFoundException("Guild not found");

            if (guild.LeaderId != r.UserId) throw new UnauthorizedAccessException("Forbidden: Only leader can update");

            guild.UpdateInfo(r.Request.Name, r.Request.Description, r.Request.Specialization, r.Request.IsAcceptingMembers, r.Request.MaxMembers, r.Request.MinProfileScore);

            await _repo.UpdateAsync(guild);

            return _mapper.Map<GuildResponse>(guild);
        }
    }
}