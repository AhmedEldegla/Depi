using MediatR;
namespace DEPI.Application.UseCases.Profiles.DeleteServicePackage;
public record DeleteServicePackageCommand(Guid Id) : IRequest<bool>;