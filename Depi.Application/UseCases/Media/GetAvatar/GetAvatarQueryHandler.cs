// Media/GetAvatar/GetAvatarQueryHandler.cs
using AutoMapper;
using DEPI.Application.DTOs.Media;
using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Media.GetAvatar;
public class GetAvatarQueryHandler : IRequestHandler<GetAvatarQuery, MediaFileResponse?>
{
    private readonly IMediaFileRepository _mediaRepository;
    private readonly IMapper _mapper;
    public GetAvatarQueryHandler(IMediaFileRepository mediaRepository, IMapper mapper) { _mediaRepository = mediaRepository; _mapper = mapper; }
    public async Task<MediaFileResponse?> Handle(GetAvatarQuery request, CancellationToken cancellationToken)
    {
        var avatar = await _mediaRepository.GetAvatarAsync(request.UserId, cancellationToken);
        if (avatar == null) return null;
        return _mapper.Map<MediaFileResponse>(avatar);
    }
}