using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Repositories.Proposals;
using MediatR;

namespace DEPI.Application.UseCases.Proposals.Queries;

public record GetMyProposalsQuery(Guid FreelancerId) : IRequest<List<ProposalResponse>>;

public class GetMyProposalsQueryHandler : IRequestHandler<GetMyProposalsQuery, List<ProposalResponse>>
{
    private readonly IProposalRepository _proposalRepository;

    public GetMyProposalsQueryHandler(IProposalRepository proposalRepository)
    {
        _proposalRepository = proposalRepository;
    }

    public async Task<List<ProposalResponse>> Handle(GetMyProposalsQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _proposalRepository.GetByFreelancerAsync(request.FreelancerId);
        return proposals.Select(MapToResponse).ToList();
    }

    private static ProposalResponse MapToResponse(Domain.Entities.Proposals.Proposal proposal)
    {
        return new ProposalResponse(
            Id: proposal.Id,
            ProjectId: proposal.ProjectId,
            ProjectTitle: proposal.Project?.Title ?? "Unknown",
            FreelancerId: proposal.FreelancerId,
            FreelancerName: proposal.Freelancer?.FullName ?? "Unknown",
            ProposedAmount: proposal.ProposedAmount,
            EstimatedDays: proposal.EstimatedDays,
            CoverLetter: proposal.CoverLetter,
            Status: proposal.Status,
            RejectionReason: proposal.RejectionReason,
            AcceptedAt: proposal.AcceptedAt,
            CreatedAt: proposal.CreatedAt,
            UpdatedAt: proposal.UpdatedAt
        );
    }
}

public record GetProposalsByProjectQuery(Guid ProjectId) : IRequest<List<ProposalResponse>>;

public class GetProposalsByProjectQueryHandler : IRequestHandler<GetProposalsByProjectQuery, List<ProposalResponse>>
{
    private readonly IProposalRepository _proposalRepository;

    public GetProposalsByProjectQueryHandler(IProposalRepository proposalRepository)
    {
        _proposalRepository = proposalRepository;
    }

    public async Task<List<ProposalResponse>> Handle(GetProposalsByProjectQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _proposalRepository.GetByProjectAsync(request.ProjectId);
        return proposals.Select(MapToResponse).ToList();
    }

    private static ProposalResponse MapToResponse(Domain.Entities.Proposals.Proposal proposal)
    {
        return new ProposalResponse(
            Id: proposal.Id,
            ProjectId: proposal.ProjectId,
            ProjectTitle: proposal.Project?.Title ?? "Unknown",
            FreelancerId: proposal.FreelancerId,
            FreelancerName: proposal.Freelancer?.FullName ?? "Unknown",
            ProposedAmount: proposal.ProposedAmount,
            EstimatedDays: proposal.EstimatedDays,
            CoverLetter: proposal.CoverLetter,
            Status: proposal.Status,
            RejectionReason: proposal.RejectionReason,
            AcceptedAt: proposal.AcceptedAt,
            CreatedAt: proposal.CreatedAt,
            UpdatedAt: proposal.UpdatedAt
        );
    }
}