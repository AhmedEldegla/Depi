using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Messaging;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Messaging.Queries;

public record GetConversationsQuery(Guid UserId) : IRequest<ConversationListResponse>;

public class GetConversationsQueryHandler : IRequestHandler<GetConversationsQuery, ConversationListResponse>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMapper _mapper;

    public GetConversationsQueryHandler(IConversationRepository conversationRepository, IMapper mapper)
    {
        _conversationRepository = conversationRepository;
        _mapper = mapper;
    }

    public async Task<ConversationListResponse> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
    {
        var conversations = await _conversationRepository.GetByUserIdAsync(request.UserId);

        var responses = conversations
            .OrderByDescending(c => c.LastMessageAt ?? c.CreatedAt)
            .Select(c =>
            {
                var response = _mapper.Map<ConversationResponse>(c);
                response.Participants = c.Participants.Select(p =>
                {
                    var participant = _mapper.Map<ParticipantResponse>(p);
                    participant.UserName = p.User?.FullName ?? "Unknown";
                    return participant;
                }).ToList();
                return response;
            }).ToList();

        return new ConversationListResponse
        {
            Conversations = responses,
            TotalCount = responses.Count
        };
    }
}

public record GetConversationMessagesQuery(Guid ConversationId, Guid CurrentUserId, int Page, int PageSize) : IRequest<MessageListResponse>;

public class GetConversationMessagesQueryHandler : IRequestHandler<GetConversationMessagesQuery, MessageListResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly IMapper _mapper;

    public GetConversationMessagesQueryHandler(
        IMessageRepository messageRepository,
        IConversationRepository conversationRepository,
        IMapper mapper)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
        _mapper = mapper;
    }

    public async Task<MessageListResponse> Handle(GetConversationMessagesQuery request, CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetWithParticipantsAsync(request.ConversationId)
            ?? throw new KeyNotFoundException(Errors.NotFound("Conversation"));

        var isParticipant = conversation.Participants.Any(p => p.UserId == request.CurrentUserId);
        if (!isParticipant)
            throw new UnauthorizedAccessException(Errors.Forbidden());

        var messages = await _messageRepository.GetByConversationAsync(
            request.ConversationId, 
            (request.Page - 1) * request.PageSize, 
            request.PageSize);

        var responses = messages
            .OrderBy(m => m.CreatedAt)
            .Select(m =>
            {
                var response = _mapper.Map<MessageResponse>(m);
                response.SenderName = m.Sender?.FullName ?? "Unknown";
                response.ReplyToContent = m.ReplyToMessage?.Content;
                response.Attachments = m.Attachments.Select(a => _mapper.Map<AttachmentResponse>(a)).ToList();
                return response;
            }).ToList();

        return new MessageListResponse
        {
            Messages = responses,
            TotalCount = responses.Count,
            HasMore = responses.Count == request.PageSize
        };
    }
}

public record GetNotificationsQuery(Guid UserId, bool UnreadOnly) : IRequest<NotificationListResponse>;

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

public record MarkNotificationReadCommand(Guid NotificationId, Guid UserId) : IRequest<Unit>;

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