// Media/DeleteMediaFile/DeleteMediaFileCommand.cs
using MediatR;
namespace DEPI.Application.UseCases.Media.DeleteMediaFile;
public record DeleteMediaFileCommand(Guid MediaFileId) : IRequest<bool>;