using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetActiveServicePackages;
public record GetActiveServicePackagesQuery() : IRequest<IEnumerable<ServicePackageResponse>>;