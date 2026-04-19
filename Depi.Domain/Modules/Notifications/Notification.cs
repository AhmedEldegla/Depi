namespace DEPI.Domain.Entities.Notifications;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Notification : AuditableEntity
{
    public Guid UserId { get;set; }
    public string Title { get;set; } = string.Empty;
    public string Message { get;set; } = string.Empty;
    public NotificationType Type { get;set; }
    public NotificationPriority Priority { get;set; }
    public Guid? RelatedEntityId { get;set; }
    public string? RelatedEntityType { get;set; }
    public bool IsRead { get;set; }
    public DateTime? ReadAt { get;set; }
    public string? ActionUrl { get;set; }
    public string? Icon { get;set; }

    public User? User { get;set; }

    private Notification() { }

    public static Notification Create(
        Guid userId,
        string title,
        string message,
        NotificationType type,
        NotificationPriority priority = NotificationPriority.Normal,
        Guid? relatedEntityId = null,
        string? relatedEntityType = null,
        string? actionUrl = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID is required");

        return new Notification
        {
            UserId = userId,
            Title = title.Trim(),
            Message = message.Trim(),
            Type = type,
            Priority = priority,
            RelatedEntityId = relatedEntityId,
            RelatedEntityType = relatedEntityType,
            ActionUrl = actionUrl,
            IsRead = false
        };
    }

    public void MarkAsRead()
    {
        if (!IsRead)
        {
            IsRead = true;
            ReadAt = DateTime.UtcNow;
        }
    }

    public void MarkAsUnread()
    {
        IsRead = false;
        ReadAt = null;
    }
}

public enum NotificationType
{
    ProjectCreated = 1,
    ProposalReceived = 2,
    ProposalAccepted = 3,
    ProposalRejected = 4,
    ContractStarted = 5,
    MilestoneStarted = 6,
    MilestoneSubmitted = 7,
    MilestoneApproved = 8,
    MilestoneRejected = 9,
    PaymentReceived = 10,
    PaymentSent = 11,
    ReviewReceived = 12,
    MessageReceived = 13,
    SystemAlert = 14,
    SecurityAlert = 15
}

public enum NotificationPriority
{
    Low = 0,
    Normal = 1,
    High = 2,
    Urgent = 3
}
