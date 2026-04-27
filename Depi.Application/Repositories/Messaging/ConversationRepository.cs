using DEPI.Domain.Entities.Messaging;
using DEPI.Domain.Interfaces;

namespace Depi.Application.Repositories.Messaging;

public interface IConversationRepository : IRepository<Conversation>
{
    Task<List<Conversation>> GetUserConversationsAsync(Guid userId, CancellationToken ct);

    Task<Conversation?> GetWithParticipantsAsync(Guid id);
}