// Media/GetAvatar/GetAvatarQuery.cs
using DEPI.Application.DTOs.Media;
using MediatR;
namespace DEPI.Application.UseCases.Media.GetAvatar;
public record GetAvatarQuery(Guid UserId) : IRequest<MediaFileResponse?>;