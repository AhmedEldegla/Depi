// Proposals/RejectProposal/RejectProposalCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Proposals;
namespace DEPI.Application.UseCases.Proposals.RejectProposal;
public class RejectProposalCommandHandler : IRequestHandler<RejectProposalCommand, ProposalResponse>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IMapper _mapper;
    public RejectProposalCommandHandler(IProposalRepository proposalRepository, IMapper mapper) { _proposalRepository = proposalRepository; _mapper = mapper; }
    public async Task<ProposalResponse> Handle(RejectProposalCommand request, CancellationToken cancellationToken)
    {
        var proposal = await _proposalRepository.GetByIdAsync(request.ProposalId) ?? throw new KeyNotFoundException(Errors.NotFound("Proposal"));
        proposal.Reject(request.Reason);
        await _proposalRepository.UpdateAsync(proposal);
        return _mapper.Map<ProposalResponse>(proposal);
    }
}