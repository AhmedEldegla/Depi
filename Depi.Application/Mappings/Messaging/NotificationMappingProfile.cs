using AutoMapper;
using DEPI.Application.DTOs.Messaging;
using DEPI.Domain.Entities.Messaging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DEPI.Application.Mappings.Messaging;

public sealed class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<Notification, NotificationResponse>();
    }
}