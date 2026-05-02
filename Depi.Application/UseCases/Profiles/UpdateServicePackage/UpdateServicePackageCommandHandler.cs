using AutoMapper;
using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Profiles.UpdateServicePackage;

public class UpdateServicePackageCommandHandler : IRequestHandler<UpdateServicePackageCommand, ServicePackageResponse>
{
    private readonly IServicePackageRepository _repository;
    private readonly IMapper _mapper;

    public UpdateServicePackageCommandHandler(IServicePackageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServicePackageResponse> Handle(UpdateServicePackageCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("حزمة الخدمة غير موجودة");

        item.Update(request.Name, request.Description, request.Price, request.DeliveryDays, request.Revisions);
        await _repository.UpdateAsync(item, cancellationToken);

        var response = _mapper.Map<ServicePackageResponse>(item);
        response.CurrencyCode = item.Currency?.Code;
        return response;
    }
}