using AutoMapper;
using DEPI.Application.DTOs.Reviews;
using DEPI.Domain.Entities.Reviews;

namespace DEPI.Application.MappingProfiles;

public class ReviewsMappingProfile : Profile
{
    public ReviewsMappingProfile()
    {
        CreateMap<Review, ReviewResponse>();

        CreateMap<CreateReviewRequestDto, Review>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Response, opt => opt.Ignore())
            .ForMember(dest => dest.ResponseAt, opt => opt.Ignore())
            .ForMember(dest => dest.Reviewer, opt => opt.Ignore())
            .ForMember(dest => dest.Reviewee, opt => opt.Ignore())
            .ForMember(dest => dest.Project, opt => opt.Ignore())
            .ForMember(dest => dest.Contract, opt => opt.Ignore());
    }
}