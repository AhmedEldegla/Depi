// Proposals/SubmitProposal/SubmitProposalCommand.cs
using DEPI.Application.DTOs.Proposals;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.SubmitProposal;
public record SubmitProposalCommand(Guid FreelancerId, SubmitProposalRequest Request) : IRequest<ProposalResponse>;