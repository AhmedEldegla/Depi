using MediatR;
namespace DEPI.Application.UseCases.Profiles.DeletePortfolioItem;
public record DeletePortfolioItemCommand(Guid Id) : IRequest<bool>;