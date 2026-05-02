using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetFeaturedPortfolio;
public record GetFeaturedPortfolioQuery : IRequest<IEnumerable<PortfolioItemResponse>>;