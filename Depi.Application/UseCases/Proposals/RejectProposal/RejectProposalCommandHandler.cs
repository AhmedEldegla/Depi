using DEPI.Application.Common;
using DEPI.Application.DTOs.Proposals;
using MediatR;
using DEPI.Domain.Entities.Proposals;
using DEPI.Application.Repositories.Proposals;
namespace DEPI.Application.UseCases.Proposals.RejectProposal;
public class RejectProposalCommandHandler : IRequestHandler<RejectProposalCommand, ProposalResponse>
{
    private readonly IProposalRepository _proposalRepository;
    public RejectProposalCommandHandler(IProposalRepository proposalRepository) { _proposalRepository = proposalRepository; }
    public async Task<ProposalResponse> Handle(RejectProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.GetByIdAsync(request.ProposalId) ?? throw new KeyNotFoundException(Errors.NotFound("Proposal"));
        proposal.Reject(request.Reason);
        await _proposalRepository.UpdateAsync(proposal);
        return MapToResponse(proposal);
    }
    private static ProposalResponse MapToResponse(Proposal proposal) => new ProposalResponse(Id: proposal.Id, ProjectId: proposal.ProjectId, ProjectTitle: proposal.Project?.Title ?? "Unknown", FreelancerId: proposal.FreelancerId, FreelancerName: proposal.Freelancer?.FullName ?? "Unknown", ProposedAmount: proposal.ProposedAmount, EstimatedDays: proposal.EstimatedDays, CoverLetter: proposal.CoverLetter, Status: proposal.Status, RejectionReason: proposal.RejectionReason, AcceptedAt: proposal.AcceptedAt, CreatedAt: proposal.CreatedAt, UpdatedAt: proposal.UpdatedAt);
}