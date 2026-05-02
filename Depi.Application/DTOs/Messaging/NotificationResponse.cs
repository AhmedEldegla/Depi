namespace DEPI.Application.DTOs.Messaging;

using DEPI.Domain.Entities.Messaging;

public sealed record NotificationResponse(
    Guid Id,
    Guid UserId,
    string Title,
    string Message,
    NotificationType Type,
    Guid? RelatedEntityId,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedAt);