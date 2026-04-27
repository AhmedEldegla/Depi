using DEPI.Domain.Entities.Messaging;
using DEPI.Domain.Interfaces;

namespace Depi.Application.Repositories.Messaging;

public interface IMessageRepository : IRepository<Message>
{
    Task<List<Message>> GetMessagesByConversationIdAsync(Guid conversationId, CancellationToken ct);

    Task<Message?> GetLastMessageAsync(Guid conversationId);
    Task<List<Message>> GetByConversationIdAsync(Guid conversationId);
}