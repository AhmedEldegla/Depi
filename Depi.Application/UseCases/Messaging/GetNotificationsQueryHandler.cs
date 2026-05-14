using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using DEPI.Application.Interfaces;
using MediatR;
using Depi.Application.UseCases.Messaging;

namespace Depi.Application.UseCases.Messaging
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, NotificationListResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<NotificationListResponse> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetByUserIdAsync(request.UserId, request.UnreadOnly);
            var unreadCount = await _notificationRepository.GetUnreadCountAsync(request.UserId);

            var responses = notifications
                .OrderByDescending(n => n.CreatedAt)
                .Select(n => _mapper.Map<NotificationResponse>(n))
                .ToList();

            return new NotificationListResponse
            {
                Notifications = responses,
                UnreadCount = unreadCount
            };
        }
    }
}
