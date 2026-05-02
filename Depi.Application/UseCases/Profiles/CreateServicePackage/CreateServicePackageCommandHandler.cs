using AutoMapper;
using DEPI.Application.UseCases.Profiles.CreateServicePackage;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Profiles;
using MediatR;
public class CreateServicePackageCommandHandler : IRequestHandler<CreateServicePackageCommand, ServicePackageResponse>
{
    private readonly IServicePackageRepository _repository;
    private readonly IMapper _mapper;
    public CreateServicePackageCommandHandler(IServicePackageRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<ServicePackageResponse> Handle(CreateServicePackageCommand request, CancellationToken cancellationToken)
    {
        var item = ServicePackage.Create(request.UserId, request.Name, request.Description, request.Price, request.DeliveryDays, request.Revisions);
        await _repository.AddAsync(item, cancellationToken);
        return _mapper.Map<ServicePackageResponse>(item);
    }
}