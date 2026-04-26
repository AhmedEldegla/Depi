// Proposals/SubmitProposal/SubmitProposalCommandHandler.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Proposals;
using MediatR;
using DEPI.Domain.Entities.Proposals;
using DEPI.Domain.Enums;
using Depi.Application.Repositories.Identity;
using DEPI.Application.Repositories.Proposals;
using DEPI.Application.Repositories.Projects;

namespace DEPI.Application.UseCases.Proposals.SubmitProposal;
public class SubmitProposalCommandHandler : IRequestHandler<SubmitProposalCommand, ProposalResponse>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    public SubmitProposalCommandHandler(IProposalRepository proposalRepository, IProjectRepository projectRepository, IUserRepository userRepository) { _proposalRepository = proposalRepository; _projectRepository = projectRepository; _userRepository = userRepository; }
    public async Task<ProposalResponse> Handle(SubmitProposalCommand request, CancellationToken cancellationToken)
    {
        var freelancer = await _userRepository.GetByIdAsync(request.FreelancerId) ?? throw new KeyNotFoundException(Errors.NotFound("User"));
        freelancer.EnsureVerifiedFor("تقديم العرض");
        var project = await _projectRepository.GetByIdAsync(request.Request.ProjectId) ?? throw new KeyNotFoundException(Errors.NotFound("Project"));
        if (project.Status != ProjectStatus.Open) throw new InvalidOperationException("Project is not open for proposals");
        var existingProposal = await _proposalRepository.GetByProjectAndFreelancerAsync(request.Request.ProjectId, request.FreelancerId);
        if (existingProposal != null) throw new InvalidOperationException(Errors.AlreadyExists("Proposal"));
        var proposal = Proposal.Create(request.Request.ProjectId, request.FreelancerId, request.Request.ProposedAmount, request.Request.EstimatedDays, request.Request.CoverLetter);
        await _proposalRepository.AddAsync(proposal);
        project.IncrementProposals();
        await _projectRepository.UpdateAsync(project);
        return MapToResponse(proposal);
    }
    private static ProposalResponse MapToResponse(Proposal proposal) => new ProposalResponse(Id: proposal.Id, ProjectId: proposal.ProjectId, ProjectTitle: proposal.Project?.Title ?? "Unknown", FreelancerId: proposal.FreelancerId, FreelancerName: proposal.Freelancer?.FullName ?? "Unknown", ProposedAmount: proposal.ProposedAmount, EstimatedDays: proposal.EstimatedDays, CoverLetter: proposal.CoverLetter, Status: proposal.Status, RejectionReason: proposal.RejectionReason, AcceptedAt: proposal.AcceptedAt, CreatedAt: proposal.CreatedAt, UpdatedAt: proposal.UpdatedAt);
}