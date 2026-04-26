// Proposals/AcceptProposal/AcceptProposalCommand.cs
using DEPI.Application.DTOs.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.AcceptProposal;
public record AcceptProposalCommand(Guid ProposalId, Guid ClientId) : IRequest<ProposalResponse>;