namespace DEPI.Domain.Entities.Messaging;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Message : AuditableEntity
{
    public DateTime SentAt;

    public Guid ConversationId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public MessageType Type { get; private set; } = MessageType.Text;
    public MessageStatus Status { get; private set; } = MessageStatus.Sent;
    public Guid? ReplyToMessageId { get; private set; }
    public DateTime? ReadAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public virtual Conversation? Conversation { get; private set; }
    public virtual User? Sender { get; private set; }
    public virtual Message? ReplyToMessage { get; private set; }
    public virtual ICollection<MessageAttachment> Attachments { get; private set; } = new List<MessageAttachment>();

    private Message() { }

    public static Message Create(
        Guid conversationId,
        Guid senderId,
        string content,
        MessageType type = MessageType.Text)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("المحتوى مطلوب", nameof(content));

        return new Message
        {
            ConversationId = conversationId,
            SenderId = senderId,
            Content = content.Trim(),
            Type = type,
            Status = MessageStatus.Sent
        };
    }

    public void MarkAsRead()
    {
        if (Status == MessageStatus.Sent)
            Status = MessageStatus.Delivered;

        ReadAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status == MessageStatus.Sent)
            Status = MessageStatus.Delivered;
    }

    public void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }

    public void UpdateContent(string newContent)
    {
        if (string.IsNullOrWhiteSpace(newContent))
            throw new ArgumentException("المحتوى مطلوب", nameof(newContent));

        if (DeletedAt.HasValue)
            throw new InvalidOperationException("لا يمكن تحديث رسالة محذوفة");

        Content = newContent.Trim();
    }
}

public enum MessageType
{
    Text = 1,
    Image = 2,
    File = 3,
    System = 4
}

public enum MessageStatus
{
    Sent = 1,
    Delivered = 2,
    Read = 3,
    Deleted = 4
}

public class MessageAttachment : AuditableEntity
{
    public Guid MessageId { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string FileUrl { get; private set; } = string.Empty;
    public string FileType { get; private set; } = string.Empty;
    public long FileSize { get; private set; }

    public virtual Message? Message { get; private set; }

    private MessageAttachment() { }

    public static MessageAttachment Create(
        Guid messageId,
        string fileName,
        string fileUrl,
        string fileType,
        long fileSize)
    {
        return new MessageAttachment
        {
            MessageId = messageId,
            FileName = fileName,
            FileUrl = fileUrl,
            FileType = fileType,
            FileSize = fileSize
        };
    }
}