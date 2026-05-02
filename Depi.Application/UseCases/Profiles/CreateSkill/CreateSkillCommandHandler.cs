using AutoMapper;
using DEPI.Application.UseCases.Profiles.CreateSkill;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Profiles;
using MediatR;
public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillResponse>
{
    private readonly ISkillRepository _repository;
    private readonly IMapper _mapper;
    public CreateSkillCommandHandler(ISkillRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<SkillResponse> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var item = Skill.Create(request.Name, request.NameEn ?? "", request.Description, request.IsVerified, request.DisplayOrder ?? 0);
        await _repository.AddAsync(item, cancellationToken);
        return _mapper.Map<SkillResponse>(item);
    }
}