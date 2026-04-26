// Proposals/AcceptProposal/AcceptProposalCommandHandler.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Proposals;
using MediatR;
using DEPI.Domain.Entities.Proposals;
using DEPI.Application.Repositories.Proposals;
using DEPI.Application.Repositories.Projects;
namespace DEPI.Application.UseCases.Proposals.AcceptProposal;
public class AcceptProposalCommandHandler : IRequestHandler<AcceptProposalCommand, ProposalResponse>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IProjectRepository _projectRepository;
    public AcceptProposalCommandHandler(IProposalRepository proposalRepository, IProjectRepository projectRepository) { _proposalRepository = proposalRepository; _projectRepository = projectRepository; }
    public async Task<ProposalResponse> Handle(AcceptProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.GetByIdAsync(request.ProposalId) ?? throw new KeyNotFoundException(Errors.NotFound("Proposal"));
        var project = await _projectRepository.GetByIdAsync(proposal.ProjectId) ?? throw new KeyNotFoundException(Errors.NotFound("Project"));
        if (project.OwnerId != request.ClientId) throw new UnauthorizedAccessException(Errors.Forbidden());
        proposal.Accept();
        await _proposalRepository.UpdateAsync(proposal);
        project.AssignFreelancer(proposal.FreelancerId, proposal.ProposedAmount);
        await _projectRepository.UpdateAsync(project);
        return MapToResponse(proposal);
    }
    private static ProposalResponse MapToResponse(Proposal proposal) => new ProposalResponse(Id: proposal.Id, ProjectId: proposal.ProjectId, ProjectTitle: proposal.Project?.Title ?? "Unknown", FreelancerId: proposal.FreelancerId, FreelancerName: proposal.Freelancer?.FullName ?? "Unknown", ProposedAmount: proposal.ProposedAmount, EstimatedDays: proposal.EstimatedDays, CoverLetter: proposal.CoverLetter, Status: proposal.Status, RejectionReason: proposal.RejectionReason, AcceptedAt: proposal.AcceptedAt, CreatedAt: proposal.CreatedAt, UpdatedAt: proposal.UpdatedAt);
}