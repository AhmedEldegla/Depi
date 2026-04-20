namespace DEPI.Domain.Entities.Messaging;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Message : AuditableEntity
{
    public Guid ConversationId { get;set; }
    public Guid SenderId { get;set; }
    public string Content { get;set; } = string.Empty;
    public string Type { get;set; } = "text";
    public string? AttachmentUrl { get;set; }
    public string? AttachmentType { get;set; }
    public string? AttachmentName { get;set; }
    public bool IsEdited { get;set; }
    public DateTime? EditedAt { get;set; }
    public new bool IsDeleted { get;set; }
    public new DateTime? DeletedAt { get;set; }
    public Guid? ReplyToMessageId { get;set; }
    public MessageStatus Status { get;set; }
    public Dictionary<string, string> Metadata { get;set; } = new();

    public Conversation? Conversation { get;set; }
    public User? Sender { get;set; }
    public Message? ReplyToMessage { get;set; }
    public ICollection<MessageReaction> Reactions { get;set; } = new HashSet<MessageReaction>();

    private Message() { }

    public static Message Create(
        Guid conversationId,
        Guid senderId,
        string content,
        string type = "text",
        Guid? replyToMessageId = null)
    {
        return new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            Content = content.Trim(),
            Type = type,
            ReplyToMessageId = replyToMessageId,
            Status = MessageStatus.Sent
        };
    }

    public static Message CreateAttachment(
        Guid conversationId,
        Guid senderId,
        string content,
        string attachmentUrl,
        string attachmentType,
        string attachmentName)
    {
        return new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            Content = content.Trim(),
            Type = "attachment",
            AttachmentUrl = attachmentUrl,
            AttachmentType = attachmentType,
            AttachmentName = attachmentName,
            Status = MessageStatus.Sent
        };
    }

    public void Edit(string newContent)
    {
        Content = newContent.Trim();
        IsEdited = true;
        EditedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        Content = "This message was deleted";
    }

    public void Deliver()
    {
        Status = MessageStatus.Delivered;
    }

    public void MarkAsRead()
    {
        if (Status == MessageStatus.Delivered)
            Status = MessageStatus.Read;
    }

    public void AddReaction(Guid userId, string emoji)
    {
        var existingReaction = Reactions.FirstOrDefault(r => r.UserId == userId && r.Emoji == emoji);
        if (existingReaction != null)
            return;

        Reactions.Add(MessageReaction.Create(Id, userId, emoji));
    }

    public void RemoveReaction(Guid userId, string emoji)
    {
        var reaction = Reactions.FirstOrDefault(r => r.UserId == userId && r.Emoji == emoji);
        if (reaction != null)
            Reactions.Remove(reaction);
    }

    public void AddMetadata(string key, string value)
    {
        Metadata[key] = value;
    }
}

public class MessageReaction : BaseEntity
{
    public Guid MessageId { get;set; }
    public Guid UserId { get;set; }
    public string Emoji { get;set; } = string.Empty;
    public DateTime CreatedAt { get;set; }

    public Message? Message { get;set; }
    public User? User { get;set; }

    private MessageReaction() { }

    public static MessageReaction Create(Guid messageId, Guid userId, string emoji)
    {
        return new MessageReaction
        {
            MessageId = messageId,
            UserId = userId,
            Emoji = emoji,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public enum MessageStatus
{
    Sent = 1,
    Delivered = 2,
    Read = 3,
    Failed = 4
}
