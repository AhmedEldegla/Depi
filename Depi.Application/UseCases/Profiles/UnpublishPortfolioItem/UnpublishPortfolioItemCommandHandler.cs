using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.UnpublishPortfolioItem;
public class UnpublishPortfolioItemCommandHandler : IRequestHandler<UnpublishPortfolioItemCommand, bool>
{
    private readonly IPortfolioItemRepository _repository;
    public UnpublishPortfolioItemCommandHandler(IPortfolioItemRepository repository) { _repository = repository; }
    public async Task<bool> Handle(UnpublishPortfolioItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("عنصر المحفظة غير موجود");
        item.Unpublish();
        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}