using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Repositories.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.GetProposalsByProject;
public class GetProposalsByProjectQueryHandler : IRequestHandler<GetProposalsByProjectQuery, IEnumerable<ProposalResponse>>
{
    private readonly IProposalRepository _repository;
    public GetProposalsByProjectQueryHandler(IProposalRepository repository) 
    { 
        _repository = repository;
    }
    public async Task<IEnumerable<ProposalResponse>> Handle(GetProposalsByProjectQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _repository.GetByProjectAsync(request.ProjectId);
        return proposals.Select(p => new ProposalResponse(
            p.Id, p.ProjectId, p.Project?.Title ?? "", p.FreelancerId, p.Freelancer?.FirstName ?? "",
            p.ProposedAmount, p.EstimatedDays, p.CoverLetter, p.Status, p.RejectionReason, p.AcceptedAt, p.CreatedAt, p.UpdatedAt));
    }
}