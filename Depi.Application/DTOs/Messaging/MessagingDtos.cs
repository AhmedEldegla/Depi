using DEPI.Domain.Entities.Messaging;

namespace DEPI.Application.DTOs.Messaging;

public record CreateConversationRequest(
    Guid UserId,
    string? Title,
    bool IsGroup,
    Guid? ProjectId
);

public record SendMessageRequest(
    Guid ConversationId,
    string Content,
    MessageType Type,
    Guid? ReplyToMessageId
);

public record AddParticipantRequest(
    Guid UserId,
    string Role
);

public class ConversationResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public Guid? ProjectId { get; set; }
    public bool IsGroup { get; set; }
    public DateTime? LastMessageAt { get; set; }
    public List<ParticipantResponse> Participants { get; set; } = new();
    public int UnreadCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ParticipantResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime? LastReadAt { get; set; }
    public bool IsMuted { get; set; }
}

public class MessageResponse
{
    public Guid Id { get; set; }
    public Guid ConversationId { get; set; }
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public MessageType Type { get; set; }
    public MessageStatus Status { get; set; }
    public Guid? ReplyToMessageId { get; set; }
    public string? ReplyToContent { get; set; }
    public DateTime? ReadAt { get; set; }
    public List<AttachmentResponse> Attachments { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}

public class AttachmentResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
}

public class NotificationResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public Guid? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ConversationListResponse
{
    public List<ConversationResponse> Conversations { get; set; } = new();
    public int TotalCount { get; set; }
}

public class MessageListResponse
{
    public List<MessageResponse> Messages { get; set; } = new();
    public int TotalCount { get; set; }
    public bool HasMore { get; set; }
}

public class NotificationListResponse
{
    public List<NotificationResponse> Notifications { get; set; } = new();
    public int UnreadCount { get; set; }
}