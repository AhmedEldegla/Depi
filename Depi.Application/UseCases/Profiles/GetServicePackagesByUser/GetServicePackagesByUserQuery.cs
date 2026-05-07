using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetServicePackagesByUser;
public record GetServicePackagesByUserQuery(Guid UserId) : IRequest<IEnumerable<ServicePackageResponse>>;