using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetServicePackageActive;
public record SetServicePackageActiveCommand(Guid Id, bool IsActive) : IRequest<bool>;