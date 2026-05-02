using DEPI.Domain.Entities.Messaging;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Interfaces;

public interface IConversationRepository : IRepository<Conversation>
{
    Task<Conversation?> GetWithParticipantsAsync(Guid conversationId);
    Task<IEnumerable<Conversation>> GetByUserIdAsync(Guid userId);
    Task<Conversation?> GetByParticipantsAsync(Guid user1Id, Guid user2Id);
}

public interface IMessageRepository : IRepository<Message>
{
    Task<Message?> GetWithAttachmentsAsync(Guid messageId);
    Task<IEnumerable<Message>> GetByConversationAsync(Guid conversationId, int skip, int take);
    Task<int> GetUnreadCountAsync(Guid conversationId, Guid userId);
}

public interface INotificationRepository : IRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId, bool unreadOnly = false);
    Task<int> GetUnreadCountAsync(Guid userId);
    Task MarkAllAsReadAsync(Guid userId);
}