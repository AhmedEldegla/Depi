// Profiles/CreateUserProfile/CreateUserProfileCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.CreateUserProfile;
public record CreateUserProfileCommand(Guid UserId, string DisplayName, string Title, string Bio, decimal HourlyRate, Guid? CurrencyId) : IRequest<Result<UserProfileResponse>>;