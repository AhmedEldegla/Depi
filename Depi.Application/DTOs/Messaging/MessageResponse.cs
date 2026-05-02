namespace DEPI.Application.DTOs.Messaging;

using DEPI.Domain.Entities.Messaging;

public sealed record MessageResponse(
    Guid Id,
    Guid ConversationId,
    Guid SenderId,
    string Content,
    MessageType Type,
    MessageStatus Status,
    Guid? ReplyToMessageId,
    DateTime? ReadAt,
    DateTime CreatedAt);