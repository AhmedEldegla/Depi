// Proposals/SubmitProposal/SubmitProposalCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Proposals;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Proposals;
using DEPI.Domain.Enums;
namespace DEPI.Application.UseCases.Proposals.SubmitProposal;
public class SubmitProposalCommandHandler : IRequestHandler<SubmitProposalCommand, ProposalResponse>
{
    private readonly IProposalRepository _proposalRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public SubmitProposalCommandHandler(IProposalRepository proposalRepository, IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper) { _proposalRepository = proposalRepository; _projectRepository = projectRepository; _userRepository = userRepository; _mapper = mapper; }
    public async Task<ProposalResponse> Handle(SubmitProposalCommand request, CancellationToken cancellationToken)
    {
        var freelancer = await _userRepository.GetByIdAsync(request.FreelancerId) ?? throw new KeyNotFoundException(Errors.NotFound("User"));
        freelancer.EnsureVerifiedFor("تقديم العرض");
        var project = await _projectRepository.GetByIdAsync(request.Request.ProjectId) ?? throw new KeyNotFoundException(Errors.NotFound("Project"));
        if (project.Status != ProjectStatus.Open) throw new InvalidOperationException("Project is not open for proposals");
        var existingProposal = await _proposalRepository.GetByProjectAndFreelancerAsync(request.Request.ProjectId, request.FreelancerId);
        if (existingProposal != null) throw new InvalidOperationException(Errors.AlreadyExists("Proposal"));
        var proposal = Proposal.Create(request.Request.ProjectId, request.FreelancerId, request.Request.ProposedAmount, request.Request.EstimatedDays, request.Request.CoverLetter);
        await _proposalRepository.AddAsync(proposal);
        project.IncrementProposals();
        await _projectRepository.UpdateAsync(project);
        return _mapper.Map<ProposalResponse>(proposal);
    }
}