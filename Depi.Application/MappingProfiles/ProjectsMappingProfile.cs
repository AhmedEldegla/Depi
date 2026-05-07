using AutoMapper;
using DEPI.Application.DTOs.Projects;
using DEPI.Domain.Entities.Projects;

namespace DEPI.Application.MappingProfiles;

public class ProjectsMappingProfile : Profile
{
    public ProjectsMappingProfile()
    {
        CreateMap<Project, ProjectResponse>()
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner != null ? src.Owner.FullName : "Unknown"))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
            .ForMember(dest => dest.AssignedFreelancerName, opt => opt.MapFrom(src => src.AssignedFreelancer != null ? src.AssignedFreelancer.FullName : null))
            .ForMember(dest => dest.EstimatedHours, opt => opt.MapFrom(src => src.RequiredLevel == ExperienceLevel.Beginner ? 10 : src.RequiredLevel == ExperienceLevel.Intermediate ? 40 : 80));

        CreateMap<CreateProjectRequest, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.ProposalsCount, opt => opt.Ignore())
            .ForMember(dest => dest.ViewsCount, opt => opt.Ignore())
            .ForMember(dest => dest.IsFeatured, opt => opt.Ignore())
            .ForMember(dest => dest.IsUrgent, opt => opt.Ignore())
            .ForMember(dest => dest.StartedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.FinalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedFreelancer, opt => opt.Ignore())
            .ForMember(dest => dest.Proposals, opt => opt.Ignore());

        CreateMap<UpdateProjectRequest, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.ProposalsCount, opt => opt.Ignore())
            .ForMember(dest => dest.ViewsCount, opt => opt.Ignore())
            .ForMember(dest => dest.IsFeatured, opt => opt.Ignore())
            .ForMember(dest => dest.IsUrgent, opt => opt.Ignore())
            .ForMember(dest => dest.StartedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.FinalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.Owner, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.AssignedFreelancer, opt => opt.Ignore())
            .ForMember(dest => dest.Proposals, opt => opt.Ignore());
    }
}