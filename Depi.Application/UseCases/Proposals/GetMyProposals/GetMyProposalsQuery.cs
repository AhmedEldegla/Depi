using DEPI.Application.DTOs.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.GetMyProposals;
public record GetMyProposalsQuery(Guid UserId) : IRequest<IEnumerable<ProposalResponse>>;