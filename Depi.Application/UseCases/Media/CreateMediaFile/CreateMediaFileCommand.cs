// Media/CreateMediaFile/CreateMediaFileCommand.cs
using DEPI.Application.DTOs.Media;
using DEPI.Domain.Enums;
using MediatR;
namespace DEPI.Application.UseCases.Media.CreateMediaFile;
public record CreateMediaFileCommand(string FileName, string OriginalName, string FilePath, string FileExtension, long FileSize, string MimeType, MediaType Type, Guid? OwnerId, string? Description) : IRequest<MediaFileResponse>;