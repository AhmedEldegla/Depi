using AutoMapper;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.DTOs.Profiles;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Profiles;

namespace DEPI.Application.MappingProfiles;

public class ProfilesMappingProfile : Profile
{
    public ProfilesMappingProfile()
    {
        CreateMap<User, UserResponse>()
          .ForMember(dest => dest.Roles, opt => opt.Ignore());

        CreateMap<UserProfile, UserProfileResponse>()
            .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency != null ? src.Currency.Code : null))
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null));

        CreateMap<ServicePackage, ServicePackageResponse>()
            .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Currency != null ? src.Currency.Code : null));

        CreateMap<PortfolioItem, PortfolioItemResponse>();
        CreateMap<Skill, SkillResponse>();

        CreateMap<FreelancerSkill, FreelancerSkillResponse>()
            .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill != null ? src.Skill.Name : null));
    }
}