using AutoMapper;
using DEPI.Application.DTOs.Messaging;
using DEPI.Domain.Entities.Messaging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DEPI.Application.Mappings.Messaging;

public sealed class ConversationMappingProfile : Profile
{
    public ConversationMappingProfile()
    {
        CreateMap<Conversation, ConversationResponse>()
            .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.Title) ? "Direct Message" : src.Title));
    }
}