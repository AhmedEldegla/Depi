using AutoMapper;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.UpdateSkill;

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, SkillResponse>
{
    private readonly ISkillRepository _repository;
    private readonly IMapper _mapper;

    public UpdateSkillCommandHandler(ISkillRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SkillResponse> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("المهارة غير موجودة");

        item.UpdateInfo(request.Name, request.NameEn, request.Description);
        if (request.IsActive && !item.IsActive)
            item.Activate();
        else if (!request.IsActive && item.IsActive)
            item.Deactivate();

        await _repository.UpdateAsync(item, cancellationToken);
        return _mapper.Map<SkillResponse>(item);
    }
}