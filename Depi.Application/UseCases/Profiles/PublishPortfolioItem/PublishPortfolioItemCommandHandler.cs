using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.PublishPortfolioItem;
public class PublishPortfolioItemCommandHandler : IRequestHandler<PublishPortfolioItemCommand, bool>
{
    private readonly IPortfolioItemRepository _repository;
    public PublishPortfolioItemCommandHandler(IPortfolioItemRepository repository) { _repository = repository; }
    public async Task<bool> Handle(PublishPortfolioItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new InvalidOperationException("عنصر المحفظة غير موجود");
        item.Publish();
        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}