using MediatR;
namespace DEPI.Application.UseCases.Profiles.ToggleFeaturedServicePackage;
public record ToggleFeaturedServicePackageCommand(Guid Id) : IRequest<bool>;