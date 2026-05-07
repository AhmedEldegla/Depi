using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.UpdatePortfolioItem;
public record UpdatePortfolioItemCommand(
    Guid Id,
    string Title,
    string Description,
    string? Url,
    string? LiveUrl
) : IRequest<PortfolioItemResponse>;