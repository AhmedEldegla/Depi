using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetServicePackagesByUser;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;
public class GetServicePackagesByUserQueryHandler : IRequestHandler<GetServicePackagesByUserQuery, IEnumerable<ServicePackageResponse>>
{
    private readonly IServicePackageRepository _repository;
    private readonly IMapper _mapper;
    public GetServicePackagesByUserQueryHandler(IServicePackageRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<ServicePackageResponse>> Handle(GetServicePackagesByUserQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return _mapper.Map<IEnumerable<ServicePackageResponse>>(items);
    }
}