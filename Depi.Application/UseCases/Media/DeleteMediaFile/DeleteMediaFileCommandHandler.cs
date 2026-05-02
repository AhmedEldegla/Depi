// Media/DeleteMediaFile/DeleteMediaFileCommandHandler.cs
using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Media.DeleteMediaFile;
public class DeleteMediaFileCommandHandler : IRequestHandler<DeleteMediaFileCommand, bool>
{
    private readonly IMediaFileRepository _mediaRepository;
    public DeleteMediaFileCommandHandler(IMediaFileRepository mediaRepository) { _mediaRepository = mediaRepository; }
    public async Task<bool> Handle(DeleteMediaFileCommand request, CancellationToken cancellationToken)
    {
        var exists = await _mediaRepository.ExistsAsync(request.MediaFileId, cancellationToken);
        if (!exists) throw new KeyNotFoundException("Media file not found");
        await _mediaRepository.DeleteAsync(request.MediaFileId, cancellationToken);
        return true;
    }
}