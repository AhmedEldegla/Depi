// Media/GetMediaFiles/GetMediaFilesQueryHandler.cs
using AutoMapper;
using DEPI.Application.DTOs.Media;
using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Media.GetMediaFiles;
public class GetMediaFilesQueryHandler : IRequestHandler<GetMediaFilesQuery, IEnumerable<MediaFileResponse>>
{
    private readonly IMediaFileRepository _mediaRepository;
    private readonly IMapper _mapper;
    public GetMediaFilesQueryHandler(IMediaFileRepository mediaRepository, IMapper mapper) { _mediaRepository = mediaRepository; _mapper = mapper; }
    public async Task<IEnumerable<MediaFileResponse>> Handle(GetMediaFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _mediaRepository.GetFilesAsync(request.UserId, request.Type, true, cancellationToken);
        return files.Select(f => _mapper.Map<MediaFileResponse>(f));
    }
}