using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.CreateServicePackage;
public record CreateServicePackageCommand(Guid UserId, string Name, string Description, decimal Price, int DeliveryDays, int Revisions) : IRequest<ServicePackageResponse>;