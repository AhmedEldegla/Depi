using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetAllSkills;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;
public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, IEnumerable<SkillResponse>>
{
    private readonly ISkillRepository _repository;
    private readonly IMapper _mapper;
    public GetAllSkillsQueryHandler(ISkillRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<SkillResponse>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync(request.IsActive, cancellationToken);
        return _mapper.Map<IEnumerable<SkillResponse>>(items);
    }
}