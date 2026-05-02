using AutoMapper;
using Depi.Application.Repositories.Messaging;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetNotificationsQuery : IRequest<Result<List<NotificationResponse>>>;

public class GetNotificationsHandler : IRequestHandler<GetNotificationsQuery, Result<List<NotificationResponse>>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public GetNotificationsHandler(
        INotificationRepository notificationRepository,
        ICurrentUserService currentUser,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<Result<List<NotificationResponse>>> Handle(GetNotificationsQuery request, CancellationToken ct)
    {
        var notes = await _notificationRepository.GetUnreadNotificationsAsync(_currentUser.UserId, ct);

        var response = _mapper.Map<List<NotificationResponse>>(notes);

        return Result<List<NotificationResponse>>.Success(response);
    }
}