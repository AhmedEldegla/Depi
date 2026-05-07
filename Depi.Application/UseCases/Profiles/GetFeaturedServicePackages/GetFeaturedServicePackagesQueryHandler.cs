using AutoMapper;
using DEPI.Application.UseCases.Profiles.GetFeaturedServicePackages;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;
public class GetFeaturedServicePackagesQueryHandler : IRequestHandler<GetFeaturedServicePackagesQuery, IEnumerable<ServicePackageResponse>>
{
    private readonly IServicePackageRepository _repository;
    private readonly IMapper _mapper;
    public GetFeaturedServicePackagesQueryHandler(IServicePackageRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<IEnumerable<ServicePackageResponse>> Handle(GetFeaturedServicePackagesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetFeaturedAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ServicePackageResponse>>(items);
    }
}