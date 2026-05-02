using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.UpdateUserProfile;
public record UpdateUserProfileCommand(
    Guid ProfileId,
    string DisplayName,
    string Title,
    string Bio,
    decimal HourlyRate,
    Guid? CountryId,
    string? LinkedInUrl,
    string? PortfolioUrl,
    string? GithubUrl,
    string? WebsiteUrl,
    DEPI.Domain.Enums.Gender Gender,
    DateTime? BirthDate,
    string? PhoneNumber,
    string? Address
) : IRequest<Result<UserProfileResponse>>;