// Proposals/AcceptProposal/AcceptProposalCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Proposals;
namespace DEPI.Application.UseCases.Proposals.AcceptProposal;
public class AcceptProposalCommandHandler : IRequestHandler<AcceptProposalCommand, ProposalResponse>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;
    public AcceptProposalCommandHandler(IProposalRepository proposalRepository, IProjectRepository projectRepository, IMapper mapper) { _proposalRepository = proposalRepository; _projectRepository = projectRepository; _mapper = mapper; }
    public async Task<ProposalResponse> Handle(AcceptProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.GetByIdAsync(request.ProposalId) ?? throw new KeyNotFoundException(Errors.NotFound("Proposal"));
        var project = await _projectRepository.GetByIdAsync(proposal.ProjectId) ?? throw new KeyNotFoundException(Errors.NotFound("Project"));
        if (project.OwnerId != request.ClientId) throw new UnauthorizedAccessException(Errors.Forbidden());
        proposal.Accept();
        await _proposalRepository.UpdateAsync(proposal);
        project.AssignFreelancer(proposal.FreelancerId, proposal.ProposedAmount);
        await _projectRepository.UpdateAsync(project);
        return _mapper.Map<ProposalResponse>(proposal);
    }
}