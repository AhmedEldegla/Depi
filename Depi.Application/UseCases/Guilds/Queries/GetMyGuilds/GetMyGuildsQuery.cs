using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using DEPI.Application.Repositories.Guilds;
using DEPI.Application.UseCases.Guilds.Contracts;

namespace DEPI.Application.UseCases.Guilds.Queries.GetGuilds
{
    public record GetGuildsQuery(string? Specialization) : IRequest<List<GuildResponse>>;

    public class GetGuildsQueryHandler : IRequestHandler<GetGuildsQuery, List<GuildResponse>>
    {
        private readonly IGuildRepository _repo;
        private readonly IMapper _mapper;

        public GetGuildsQueryHandler(IGuildRepository repo, IMapper mapper)
        {
            _repo = repo; _mapper = mapper;
        }

        public async Task<List<GuildResponse>> Handle(GetGuildsQuery r, CancellationToken ct)
        {
            var guilds = string.IsNullOrWhiteSpace(r.Specialization)
                ? await _repo.GetActiveAsync()
                : await _repo.GetBySpecializationAsync(r.Specialization);

            return _mapper.Map<List<GuildResponse>>(guilds);
        }
    }
}