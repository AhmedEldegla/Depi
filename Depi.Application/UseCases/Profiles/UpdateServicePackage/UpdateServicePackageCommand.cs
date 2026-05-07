using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.UpdateServicePackage;
public record UpdateServicePackageCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int DeliveryDays,
    int Revisions
) : IRequest<ServicePackageResponse>;