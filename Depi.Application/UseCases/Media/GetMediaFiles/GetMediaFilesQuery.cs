// Media/GetMediaFiles/GetMediaFilesQuery.cs
using DEPI.Application.DTOs.Media;
using DEPI.Domain.Enums;
using MediatR;
namespace DEPI.Application.UseCases.Media.GetMediaFiles;
public record GetMediaFilesQuery(Guid UserId, MediaType? Type, bool UserFilesOnly) : IRequest<IEnumerable<MediaFileResponse>>;