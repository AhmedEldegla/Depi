using MediatR;
namespace DEPI.Application.UseCases.Profiles.UnpublishPortfolioItem;
public record UnpublishPortfolioItemCommand(Guid Id) : IRequest<bool>;