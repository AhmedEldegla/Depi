using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.ToggleFeaturedServicePackage;
public class ToggleFeaturedServicePackageCommandHandler : IRequestHandler<ToggleFeaturedServicePackageCommand, bool>
{
    private readonly IServicePackageRepository _repository;
    public ToggleFeaturedServicePackageCommandHandler(IServicePackageRepository repository) { _repository = repository; }
    public async Task<bool> Handle(ToggleFeaturedServicePackageCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("حزمة الخدمة غير موجودة");
        item.ToggleFeatured();
        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}