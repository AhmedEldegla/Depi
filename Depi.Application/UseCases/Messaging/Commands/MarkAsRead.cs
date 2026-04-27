using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

public record MarkAsReadCommand(Guid ConversationId) : IRequest<Result<bool>>;

public class MarkAsReadHandler : IRequestHandler<MarkAsReadCommand, Result<bool>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUserService _currentUser;

    public MarkAsReadHandler(INotificationRepository notificationRepository, ICurrentUserService currentUser)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<bool>> Handle(MarkAsReadCommand request, CancellationToken ct)
    {
        var notifications = await _notificationRepository.GetUnreadNotificationsAsync(_currentUser.UserId, ct);

        var conversationNotifications = notifications.Where(n => n.RelatedEntityId == request.ConversationId);

        foreach (var notification in conversationNotifications)
        {
            notification.MarkAsRead();
            await _notificationRepository.UpdateAsync(notification);
        }

        return Result<bool>.Success(true);
    }
}