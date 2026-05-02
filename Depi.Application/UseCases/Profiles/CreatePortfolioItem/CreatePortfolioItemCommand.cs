using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.CreatePortfolioItem;
public record CreatePortfolioItemCommand(Guid UserId, string Title, string Description, string? Url, string? LiveUrl) : IRequest<PortfolioItemResponse>;