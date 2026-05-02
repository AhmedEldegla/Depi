using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetActiveServicePackages;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;
public class GetActiveServicePackagesQueryHandler : IRequestHandler<GetActiveServicePackagesQuery, IEnumerable<ServicePackageResponse>>
{
    private readonly IServicePackageRepository _repository;
    private readonly IMapper _mapper;
    public GetActiveServicePackagesQueryHandler(IServicePackageRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<ServicePackageResponse>> Handle(GetActiveServicePackagesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetActiveAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ServicePackageResponse>>(items);
    }
}