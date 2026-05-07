// Profiles/SetUserProfileLinks/SetUserProfileLinksCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.SetUserProfileLinks;
public record SetUserProfileLinksCommand(Guid UserId, string? LinkedInUrl, string? PortfolioUrl, string? GithubUrl, string? WebsiteUrl) : IRequest<Result<UserProfileResponse>>;