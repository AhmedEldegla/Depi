// Contracts/AddMilestone/AddMilestoneCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Contracts;
namespace DEPI.Application.UseCases.Contracts.AddMilestone;
public class AddMilestoneCommandHandler : IRequestHandler<AddMilestoneCommand, MilestoneResponse>
{
    private readonly IContractRepository _contractRepository;
    private readonly IMapper _mapper;
    public AddMilestoneCommandHandler(IContractRepository contractRepository, IMapper mapper) { _contractRepository = contractRepository; _mapper = mapper; }
    public async Task<MilestoneResponse> Handle(AddMilestoneCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.Request.ContractId) ?? throw new KeyNotFoundException(Errors.NotFound("Contract"));
        if (contract.FreelancerId != request.RequesterId && contract.ClientId != request.RequesterId) throw new UnauthorizedAccessException(Errors.Forbidden());
        var milestone = Milestone.Create(request.Request.ContractId, request.Request.Title, request.Request.Description, request.Request.Amount, request.Request.DueDate);
        contract.Milestones.Add(milestone);
        await _contractRepository.UpdateAsync(contract);
        return _mapper.Map<MilestoneResponse>(milestone);
    }
}