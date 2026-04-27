namespace DEPI.Domain.Entities.Messaging;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Contracts;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;

public class Conversation : AuditableEntity
{
    public string Title { get; private set; } = string.Empty;
    public Guid? ProjectId { get; private set; }
    public Guid? ContractId { get; private set; }
    public bool IsGroup { get; private set; }
    public DateTime? LastMessageAt { get; private set; }

    public virtual Project? Project { get; private set; }
    public virtual Contract? Contract { get; private set; }
    public virtual ICollection<ConversationParticipant> Participants { get; private set; } = new List<ConversationParticipant>();
    public virtual ICollection<Message> Messages { get; private set; } = new List<Message>();

    private Conversation() { }

    public static Conversation CreateDirect(Guid user1Id, Guid user2Id, Guid? projectId = null)
    {
        return new Conversation
        {
            Title = string.Empty,
            ProjectId = projectId,
            IsGroup = false
        };
    }

    public static Conversation CreateGroup(string title, Guid creatorId, Guid? projectId = null)
    {
        return new Conversation
        {
            Title = title.Trim(),
            ProjectId = projectId,
            IsGroup = true
        };
    }

    public void UpdateLastMessage()
    {
        LastMessageAt = DateTime.UtcNow;
    }

    public static Conversation Create(string? title, bool isGroup)
    {
        throw new NotImplementedException();
    }
}

public class ConversationParticipant : AuditableEntity
{
    public Guid ConversationId { get; private set; }
    public Guid UserId { get; private set; }
    public string Role { get; private set; } = "member";
    public DateTime? LastReadAt { get; private set; }
    public bool IsMuted { get; private set; }

    public virtual Conversation? Conversation { get; private set; }
    public virtual User? User { get; private set; }

    private ConversationParticipant() { }

    public static ConversationParticipant Create(Guid conversationId, Guid userId, string role = "member")
    {
        return new ConversationParticipant
        {
            ConversationId = conversationId,
            UserId = userId,
            Role = role,
            IsMuted = false
        };
    }

    public void MarkAsRead()
    {
        LastReadAt = DateTime.UtcNow;
    }

    public void Mute()
    {
        IsMuted = true;
    }

    public void Unmute()
    {
        IsMuted = false;
    }
}