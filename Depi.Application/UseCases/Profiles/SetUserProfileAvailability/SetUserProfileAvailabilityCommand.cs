// Profiles/SetUserProfileAvailability/SetUserProfileAvailabilityCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetUserProfileAvailability;
public record SetUserProfileAvailabilityCommand(Guid UserId, bool IsAvailable) : IRequest<Result<UserProfileResponse>>;