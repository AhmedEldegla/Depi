using AutoMapper;
using DEPI.Application.DTOs.Contracts;
using DEPI.Domain.Entities.Contracts;

namespace DEPI.Application.MappingProfiles;

public class ContractsMappingProfile : Profile
{
    public ContractsMappingProfile()
    {
        CreateMap<Contract, ContractResponse>()
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.Project != null ? src.Project.Title : "Unknown"))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.FullName : "Unknown"))
            .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer != null ? src.Freelancer.FullName : "Unknown"));

        CreateMap<Milestone, MilestoneResponse>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.ContractId));

        CreateMap<Contract, MilestoneResponse>()
            .ForMember(dest => dest.ContractId, opt => opt.MapFrom(src => src.Id));

        CreateMap<CreateContractRequest, Contract>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.StartedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Project, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Freelancer, opt => opt.Ignore())
            .ForMember(dest => dest.Milestones, opt => opt.Ignore());

        CreateMap<CreateMilestoneRequest, Milestone>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.StartedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CompletedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Deliverables, opt => opt.Ignore())
            .ForMember(dest => dest.Contract, opt => opt.Ignore());
    }
}