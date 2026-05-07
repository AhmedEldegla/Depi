using AutoMapper;
using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Interfaces;
using DEPI.Application.UseCases.Proposals.GetProposalsByProject;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.GetProposalsByProject;
public class GetProposalsByProjectQueryHandler : IRequestHandler<GetProposalsByProjectQuery, IEnumerable<ProposalResponse>>
{
    private readonly IProposalRepository _repository;
    private readonly IMapper _mapper;
    public GetProposalsByProjectQueryHandler(IProposalRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<ProposalResponse>> Handle(GetProposalsByProjectQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _repository.GetByProjectAsync(request.ProjectId);
        return proposals.Select(p => _mapper.Map<ProposalResponse>(p));
    }
}