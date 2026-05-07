using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.ToggleFeaturedPortfolioItem;
public class ToggleFeaturedPortfolioItemCommandHandler : IRequestHandler<ToggleFeaturedPortfolioItemCommand, bool>
{
    private readonly IPortfolioItemRepository _repository;
    public ToggleFeaturedPortfolioItemCommandHandler(IPortfolioItemRepository repository) { _repository = repository; }
    public async Task<bool> Handle(ToggleFeaturedPortfolioItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("عنصر المحفظة غير موجود");
        item.ToggleFeatured();
        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}