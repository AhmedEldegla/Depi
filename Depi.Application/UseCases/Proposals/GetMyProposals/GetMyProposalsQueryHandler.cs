using AutoMapper;
using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Interfaces;
using DEPI.Application.UseCases.Proposals.GetMyProposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.GetMyProposals;
public class GetMyProposalsQueryHandler : IRequestHandler<GetMyProposalsQuery, IEnumerable<ProposalResponse>>
{
    private readonly IProposalRepository _repository;
    private readonly IMapper _mapper;
    public GetMyProposalsQueryHandler(IProposalRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<ProposalResponse>> Handle(GetMyProposalsQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _repository.GetByFreelancerAsync(request.UserId);
        return proposals.Select(p => _mapper.Map<ProposalResponse>(p));
    }
}