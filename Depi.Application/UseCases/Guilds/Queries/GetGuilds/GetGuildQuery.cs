using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.UseCases.Guilds.Contracts;
namespace DEPI.Application.UseCases.Guild.Queries.GetGuild
{
    public record GetGuildQuery(Guid GuildId) : IRequest<GuildResponse?>;

    public class GetGuildQueryHandler : IRequestHandler<GetGuildQuery, GuildResponse?>
    {
        private readonly IGuildRepository _repo;
        private readonly IMapper _mapper;

        public GetGuildQueryHandler(IGuildRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<GuildResponse?> Handle(GetGuildQuery r, CancellationToken ct)
        {
            var guild = await _repo.GetWithMembersAsync(r.GuildId);
            return guild != null ? _mapper.Map<GuildResponse>(guild) : null;
        }
    }
}