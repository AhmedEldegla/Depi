using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Repositories.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.GetMyProposals;
public class GetMyProposalsQueryHandler : IRequestHandler<GetMyProposalsQuery, IEnumerable<ProposalResponse>>
{
    private readonly IProposalRepository _repository;
    public GetMyProposalsQueryHandler(IProposalRepository repository)
    {
        _repository = repository; 
    }
    public async Task<IEnumerable<ProposalResponse>> Handle(GetMyProposalsQuery request, CancellationToken cancellationToken)
    {
        var proposals = await _repository.GetByFreelancerAsync(request.UserId);
        return proposals.Select(p => new ProposalResponse(
            p.Id, p.ProjectId, p.Project?.Title ?? "", p.FreelancerId, p.Freelancer?.FirstName ?? "",
            p.ProposedAmount, p.EstimatedDays, p.CoverLetter, p.Status, p.RejectionReason, p.AcceptedAt, p.CreatedAt, p.UpdatedAt));
    }
}