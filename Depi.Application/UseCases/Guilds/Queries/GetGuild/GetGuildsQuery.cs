using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.UseCases.Guilds.Contracts;

namespace DEPI.Application.UseCases.Guilds.Queries.GetMyGuilds
{
    public record GetMyGuildsQuery(Guid UserId) : IRequest<List<GuildResponse>>;

    public class GetMyGuildsQueryHandler : IRequestHandler<GetMyGuildsQuery, List<GuildResponse>>
    {
        private readonly IGuildMemberRepository _memberRepo;
        private readonly IGuildRepository _guildRepo;
        private readonly IMapper _mapper;

        public GetMyGuildsQueryHandler(IGuildMemberRepository memberRepo, IGuildRepository guildRepo, IMapper mapper)
        {
            _memberRepo = memberRepo;
            _guildRepo = guildRepo;
            _mapper = mapper;
        }

        public async Task<List<GuildResponse>> Handle(GetMyGuildsQuery r, CancellationToken ct)
        {
            var memberships = await _memberRepo.GetByUserIdAsync(r.UserId);

            var guilds = new List<DEPI.Domain.Entities.Guilds.Guild>();

            foreach (var m in memberships)
            {
                var g = await _guildRepo.GetByIdAsync(m.GuildId);
                if (g != null)
                {
                    guilds.Add(g);
                }
            }

            return _mapper.Map<List<GuildResponse>>(guilds);
        }
    }
}