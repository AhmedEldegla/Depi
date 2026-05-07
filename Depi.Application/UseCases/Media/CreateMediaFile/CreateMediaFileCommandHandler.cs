// Media/CreateMediaFile/CreateMediaFileCommandHandler.cs
using AutoMapper;
using DEPI.Application.DTOs.Media;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Media;
using MediatR;
namespace DEPI.Application.UseCases.Media.CreateMediaFile;
public class CreateMediaFileCommandHandler : IRequestHandler<CreateMediaFileCommand, MediaFileResponse>
{
    private readonly IMediaFileRepository _mediaRepository;
    private readonly IMapper _mapper;
    public CreateMediaFileCommandHandler(IMediaFileRepository mediaRepository, IMapper mapper) { _mediaRepository = mediaRepository; _mapper = mapper; }
    public async Task<MediaFileResponse> Handle(CreateMediaFileCommand request, CancellationToken cancellationToken)
    {
        var mediaFile = MediaFile.Create(request.FileName, request.OriginalName, request.FilePath, request.FileExtension, request.FileSize, request.MimeType, request.Type, request.OwnerId, request.Description);
        await _mediaRepository.AddAsync(mediaFile, cancellationToken);
        return _mapper.Map<MediaFileResponse>(mediaFile);
    }
}