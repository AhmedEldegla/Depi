namespace DEPI.Domain.Entities.Messaging;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Conversation : AuditableEntity
{
    public string Type { get;set; } = "direct";
    public Guid? ProjectId { get;set; }
    public Guid? ContractId { get;set; }
    public string? Title { get;set; }
    public DateTime? LastMessageAt { get;set; }
    public Guid LastMessageBy { get;set; }
    public bool IsArchived { get;set; }
    public bool IsPinned { get;set; }

    public Projects.Project? Project { get;set; }
    public Projects.Contract? Contract { get;set; }
    public ICollection<ConversationParticipant> Participants { get;set; } = new HashSet<ConversationParticipant>();
    public ICollection<Message> Messages { get;set; } = new HashSet<Message>();

    private Conversation() { }

    public static Conversation CreateDirect(Guid user1Id, Guid user2Id, Guid createdBy)
    {
        return new Conversation
        {
            Type = "direct",
            LastMessageBy = createdBy
        };
    }

    public static Conversation CreateProject(Guid projectId, Guid createdBy)
    {
        return new Conversation
        {
            Type = "project",
            ProjectId = projectId,
            LastMessageBy = createdBy,
            Title = "Project Discussion"
        };
    }

    public static Conversation CreateContract(Guid contractId, Guid createdBy)
    {
        return new Conversation
        {
            Type = "contract",
            ContractId = contractId,
            LastMessageBy = createdBy,
            Title = "Contract Discussion"
        };
    }

    public static Conversation CreateGroup(string title, Guid createdBy)
    {
        return new Conversation
        {
            Type = "group",
            Title = title,
            LastMessageBy = createdBy
        };
    }

    public void UpdateLastMessage(Guid userId)
    {
        LastMessageAt = DateTime.UtcNow;
        LastMessageBy = userId;
    }

    public void Archive()
    {
        IsArchived = true;
    }

    public void Unarchive()
    {
        IsArchived = false;
    }

    public void Pin()
    {
        IsPinned = true;
    }

    public void Unpin()
    {
        IsPinned = false;
    }
}

public class ConversationParticipant : BaseEntity
{
    public Guid ConversationId { get;set; }
    public Guid UserId { get;set; }
    public string Role { get;set; } = "member";
    public DateTime JoinedAt { get;set; }
    public bool IsMuted { get;set; }
    public bool IsAdmin { get;set; }
    public int UnreadCount { get;set; }
    public DateTime? LastReadAt { get;set; }

    public Conversation? Conversation { get;set; }
    public User? User { get;set; }

    private ConversationParticipant() { }

    public static ConversationParticipant Create(Guid conversationId, Guid userId, string role = "member")
    {
        return new ConversationParticipant
        {
            ConversationId = conversationId,
            UserId = userId,
            Role = role,
            JoinedAt = DateTime.UtcNow,
            IsMuted = false,
            IsAdmin = role == "admin"
        };
    }

    public void MarkAsRead()
    {
        LastReadAt = DateTime.UtcNow;
        UnreadCount = 0;
    }

    public void IncrementUnread()
    {
        UnreadCount++;
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
