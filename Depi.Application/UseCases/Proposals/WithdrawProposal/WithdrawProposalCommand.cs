// Proposals/WithdrawProposal/WithdrawProposalCommand.cs
using MediatR;
namespace DEPI.Application.UseCases.Proposals.WithdrawProposal;
public record WithdrawProposalCommand(Guid ProposalId, Guid FreelancerId) : IRequest<Unit>;