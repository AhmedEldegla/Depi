using MediatR;
namespace DEPI.Application.UseCases.Profiles.PublishPortfolioItem;
public record PublishPortfolioItemCommand(Guid Id) : IRequest<bool>;