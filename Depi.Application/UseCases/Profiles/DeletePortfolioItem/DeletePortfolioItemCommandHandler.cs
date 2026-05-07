using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.DeletePortfolioItem;
public class DeletePortfolioItemCommandHandler : IRequestHandler<DeletePortfolioItemCommand, bool>
{
    private readonly IPortfolioItemRepository _repository;
    public DeletePortfolioItemCommandHandler(IPortfolioItemRepository repository) { _repository = repository; }
    public async Task<bool> Handle(DeletePortfolioItemCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsAsync(request.Id, cancellationToken);
        if (!exists) throw new InvalidOperationException("عنصر المحفظة غير موجود");
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}