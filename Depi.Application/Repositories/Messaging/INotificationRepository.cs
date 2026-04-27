using DEPI.Domain.Entities.Messaging;
using DEPI.Domain.Interfaces;

namespace Depi.Application.Repositories.Messaging;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetUnreadNotificationsAsync(Guid userId, CancellationToken ct);

    Task<List<Notification>> GetByUserIdAsync(Guid userId, CancellationToken ct);
}