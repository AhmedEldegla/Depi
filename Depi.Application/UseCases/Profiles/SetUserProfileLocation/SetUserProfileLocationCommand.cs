// Profiles/SetUserProfileLocation/SetUserProfileLocationCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetUserProfileLocation;
public record SetUserProfileLocationCommand(Guid UserId, Guid? CountryId, string? Address) : IRequest<Result<UserProfileResponse>>;