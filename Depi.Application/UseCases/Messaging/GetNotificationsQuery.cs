using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using Depi.Application.UseCases.Messaging;

using DEPI.Application.Interfaces;
using MediatR;

namespace Depi.Application.UseCases.Messaging
{
    public record GetNotificationsQuery(Guid UserId, bool UnreadOnly) : IRequest<NotificationListResponse>;

}
