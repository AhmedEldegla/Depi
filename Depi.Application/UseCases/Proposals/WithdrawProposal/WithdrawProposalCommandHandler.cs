// Proposals/WithdrawProposal/WithdrawProposalCommandHandler.cs
using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Proposals.WithdrawProposal;
public class WithdrawProposalCommandHandler : IRequestHandler<WithdrawProposalCommand, Unit>
{
    private readonly IProposalRepository _proposalRepository;
    public WithdrawProposalCommandHandler(IProposalRepository proposalRepository) { _proposalRepository = proposalRepository; }
    public async Task<Unit> Handle(WithdrawProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.GetByIdAsync(request.ProposalId) ?? throw new KeyNotFoundException(Errors.NotFound("Proposal"));
        if (proposal.FreelancerId != request.FreelancerId) throw new UnauthorizedAccessException(Errors.Forbidden());
        proposal.Withdraw();
        await _proposalRepository.UpdateAsync(proposal);
        return Unit.Value;
    }
}