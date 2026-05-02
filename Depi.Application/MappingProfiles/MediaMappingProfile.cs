using AutoMapper;
using DEPI.Application.DTOs.Media;
using DEPI.Domain.Entities.Media;

namespace DEPI.Application.MappingProfiles;

public class MediaMappingProfile : Profile
{
    public MediaMappingProfile()
    {
        CreateMap<MediaFile, MediaFileResponse>();
    }
}