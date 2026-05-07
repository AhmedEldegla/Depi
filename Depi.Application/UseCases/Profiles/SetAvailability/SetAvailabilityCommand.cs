using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetAvailability;
public record SetAvailabilityCommand(Guid ProfileId, bool IsAvailable) : IRequest<bool>;