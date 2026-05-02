using MediatR;
namespace DEPI.Application.UseCases.Profiles.ToggleFeaturedPortfolioItem;
public record ToggleFeaturedPortfolioItemCommand(Guid Id) : IRequest<bool>;