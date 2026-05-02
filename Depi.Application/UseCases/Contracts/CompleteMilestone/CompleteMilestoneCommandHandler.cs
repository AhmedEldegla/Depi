using AutoMapper;
using DEPI.Application.DTOs.Contracts;
using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Contracts.CompleteMilestone;
public class CompleteMilestoneCommandHandler : IRequestHandler<CompleteMilestoneCommand, MilestoneResponse>
{
    private readonly IMilestoneRepository _repository;
    private readonly IMapper _mapper;
    public CompleteMilestoneCommandHandler(IMilestoneRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<MilestoneResponse> Handle(CompleteMilestoneCommand request, CancellationToken cancellationToken)
    {
        var milestone = await _repository.GetByIdAsync(request.MilestoneId, cancellationToken)
            ?? throw new InvalidOperationException("المرحلة غير موجودة");
        milestone.Complete(request.Deliverables);
        await _repository.UpdateAsync(milestone, cancellationToken);
        return _mapper.Map<MilestoneResponse>(milestone);
    }
}