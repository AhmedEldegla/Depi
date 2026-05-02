using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.DeleteServicePackage;
public class DeleteServicePackageCommandHandler : IRequestHandler<DeleteServicePackageCommand, bool>
{
    private readonly IServicePackageRepository _repository;
    public DeleteServicePackageCommandHandler(IServicePackageRepository repository) { _repository = repository; }
    public async Task<bool> Handle(DeleteServicePackageCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsAsync(request.Id, cancellationToken);
        if (!exists) throw new InvalidOperationException("حزمة الخدمة غير موجودة");
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}