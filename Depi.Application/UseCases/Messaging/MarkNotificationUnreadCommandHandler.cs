using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using AutoMapper;
using DEPI.Application.DTOs.Messaging;
using DEPI.Application.UseCases.Messaging;
using MediatR;

namespace Depi.Application.UseCases.Messaging
{
    public class MarkNotificationUnreadCommandHandler
        : IRequestHandler<MarkNotificationUnreadCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationUnreadCommandHandler(
            INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(
            MarkNotificationUnreadCommand request,
            CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId)
                ?? throw new KeyNotFoundException(Errors.NotFound("Notification"));

            if (notification.UserId != request.UserId)
                throw new UnauthorizedAccessException(Errors.Forbidden());

            notification.MarkAsUnread();
            await _notificationRepository.UpdateAsync(notification);

            return Unit.Value;
        }
    }
}
