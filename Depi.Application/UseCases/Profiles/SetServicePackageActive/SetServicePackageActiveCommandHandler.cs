using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetServicePackageActive;
public class SetServicePackageActiveCommandHandler : IRequestHandler<SetServicePackageActiveCommand, bool>
{
    private readonly IServicePackageRepository _repository;
    public SetServicePackageActiveCommandHandler(IServicePackageRepository repository) { _repository = repository; }
    public async Task<bool> Handle(SetServicePackageActiveCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("حزمة الخدمة غير موجودة");
        item.SetActive(request.IsActive);
        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}