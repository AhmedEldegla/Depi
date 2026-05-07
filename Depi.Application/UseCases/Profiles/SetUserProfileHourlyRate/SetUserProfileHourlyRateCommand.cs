// Profiles/SetUserProfileHourlyRate/SetUserProfileHourlyRateCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetUserProfileHourlyRate;
public record SetUserProfileHourlyRateCommand(Guid UserId, decimal Rate, Guid CurrencyId) : IRequest<Result<UserProfileResponse>>;