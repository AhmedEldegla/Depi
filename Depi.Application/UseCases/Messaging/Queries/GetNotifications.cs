using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetNotificationsQuery : IRequest<Result<List<NotificationResponse>>>;

public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, Result<List<NotificationResponse>>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUserService _currentUser;

    public GetNotificationsHandler(INotificationRepository notificationRepository, ICurrentUserService currentUser)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
    }

    public async Task<Result<List<NotificationResponse>>> Handle(GetNotificationsQuery request, CancellationToken ct)
    {
        var notes = await _notificationRepository.GetUnreadNotificationsAsync(_currentUser.UserId, ct);

        var response = notes.Select(n => new NotificationResponse(n.Id, n.Message, n.CreatedAt)).ToList();
        return Result<List<NotificationResponse>>.Success(response);
    }
}

public record NotificationResponse(Guid Id, string Message, DateTime CreatedAt);