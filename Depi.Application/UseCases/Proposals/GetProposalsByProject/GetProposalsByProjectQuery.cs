using DEPI.Application.DTOs.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.GetProposalsByProject;
public record GetProposalsByProjectQuery(Guid ProjectId) : IRequest<IEnumerable<ProposalResponse>>;