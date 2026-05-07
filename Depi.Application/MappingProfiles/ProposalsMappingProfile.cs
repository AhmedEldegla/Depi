using AutoMapper;
using DEPI.Application.DTOs.Proposals;
using DEPI.Domain.Entities.Proposals;

namespace DEPI.Application.MappingProfiles;

public class ProposalsMappingProfile : Profile
{
    public ProposalsMappingProfile()
    {
        CreateMap<Proposal, ProposalResponse>()
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.Project != null ? src.Project.Title : "Unknown"))
            .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer != null ? src.Freelancer.FullName : "Unknown"));

        CreateMap<SubmitProposalRequest, Proposal>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.RejectionReason, opt => opt.Ignore())
            .ForMember(dest => dest.AcceptedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Project, opt => opt.Ignore())
            .ForMember(dest => dest.Freelancer, opt => opt.Ignore());
    }
}