// Proposals/RejectProposal/RejectProposalCommand.cs
using DEPI.Application.DTOs.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.RejectProposal;
public record RejectProposalCommand(Guid ProposalId, Guid ClientId, string? Reason) : IRequest<ProposalResponse>;