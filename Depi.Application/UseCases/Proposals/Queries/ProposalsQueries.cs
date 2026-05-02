using AutoMapper;
using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Proposals.Queries;

public record GetMyProposalsQuery(Guid FreelancerId) : IRequest<List<ProposalResponse>>;

public class GetMyProposalsQueryHandler : IRequestHandler<GetMyProposalsQuery, List<ProposalResponse>>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IMapper _mapper;

    public GetMyProposalsQueryHandler(IProposalRepository proposalRepository, IMapper mapper)
    {
        _proposalRepository = proposalRepository;
        _mapper = mapper;
    }

    public async Task<List<ProposalResponse>> Handle(GetMyProposalsQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _proposalRepository.GetByFreelancerAsync(request.FreelancerId);
        return _mapper.Map<List<ProposalResponse>>(proposals);
    }
}

public record GetProposalsByProjectQuery(Guid ProjectId) : IRequest<List<ProposalResponse>>;

public class GetProposalsByProjectQueryHandler : IRequestHandler<GetProposalsByProjectQuery, List<ProposalResponse>>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IMapper _mapper;

    public GetProposalsByProjectQueryHandler(IProposalRepository proposalRepository, IMapper mapper)
    {
        _proposalRepository = proposalRepository;
        _mapper = mapper;
    }

    public async Task<List<ProposalResponse>> Handle(GetProposalsByProjectQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _proposalRepository.GetByProjectAsync(request.ProjectId);
        return _mapper.Map<List<ProposalResponse>>(proposals);
    }
}