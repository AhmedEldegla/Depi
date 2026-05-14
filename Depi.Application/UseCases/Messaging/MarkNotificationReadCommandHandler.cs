using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Messaging;

    public class MarkNotificationReadCommandHandler : IRequestHandler<MarkNotificationReadCommand, Unit>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Unit> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.NotificationId)
                ?? throw new KeyNotFoundException(Errors.NotFound("Notification"));

            if (notification.UserId != request.UserId)
                throw new UnauthorizedAccessException(Errors.Forbidden());

            notification.MarkAsRead();
            await _notificationRepository.UpdateAsync(notification);

            return Unit.Value;
        }
    }

