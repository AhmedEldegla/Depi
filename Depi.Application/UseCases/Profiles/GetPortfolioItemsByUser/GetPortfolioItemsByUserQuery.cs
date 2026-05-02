using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetPortfolioItemsByUser;
public record GetPortfolioItemsByUserQuery(Guid UserId) : IRequest<IEnumerable<PortfolioItemResponse>>;