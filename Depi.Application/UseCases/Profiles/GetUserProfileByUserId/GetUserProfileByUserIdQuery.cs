using DEPI.Application.DTOs.Profiles;
using MediatR;
namespace DEPI.Application.UseCases.Profiles.GetUserProfileByUserId;
public record GetUserProfileByUserIdQuery(Guid UserId) : IRequest<UserProfileResponse?>;