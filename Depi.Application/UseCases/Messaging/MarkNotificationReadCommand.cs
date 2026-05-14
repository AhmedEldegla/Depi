using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Messaging;

    public record MarkNotificationReadCommand(Guid NotificationId, Guid UserId) : IRequest<Unit>;


