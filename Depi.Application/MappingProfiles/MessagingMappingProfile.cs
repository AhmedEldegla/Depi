using AutoMapper;
using DEPI.Application.DTOs.Messaging;
using DEPI.Domain.Entities.Messaging;

namespace DEPI.Application.MappingProfiles;

public class MessagingMappingProfile : Profile
{
    public MessagingMappingProfile()
    {
        CreateMap<Conversation, ConversationResponse>()
            .ForMember(dest => dest.Participants, opt => opt.Ignore());

        CreateMap<Message, MessageResponse>()
            .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender != null ? src.Sender.FullName : "Unknown"))
            .ForMember(dest => dest.ReplyToContent, opt => opt.MapFrom(src => src.ReplyToMessage != null ? src.ReplyToMessage.Content : null))
            .ForMember(dest => dest.Attachments, opt => opt.Ignore());

        CreateMap<Notification, NotificationResponse>();

        CreateMap<CreateConversationRequest, Conversation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastMessageAt, opt => opt.Ignore())
            .ForMember(dest => dest.Participants, opt => opt.Ignore())
            .ForMember(dest => dest.Messages, opt => opt.Ignore());

        CreateMap<SendMessageRequest, Message>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.ReadAt, opt => opt.Ignore())
            .ForMember(dest => dest.Sender, opt => opt.Ignore())
            .ForMember(dest => dest.Conversation, opt => opt.Ignore())
            .ForMember(dest => dest.ReplyToMessage, opt => opt.Ignore())
            .ForMember(dest => dest.Attachments, opt => opt.Ignore());
    }
}