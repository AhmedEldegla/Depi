using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetFeaturedServicePackages;
public record GetFeaturedServicePackagesQuery : IRequest<IEnumerable<ServicePackageResponse>>;