using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class ConversationRepository : Repository<Conversation>, IConversationRepository
{
    public ConversationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Conversation?> GetWithParticipantsAsync(Guid conversationId)
    {
        return await _dbSet
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }

    public async Task<IEnumerable<Conversation>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .OrderByDescending(c => c.LastMessageAt)
            .ToListAsync();
    }

    public async Task<Conversation?> GetByParticipantsAsync(Guid user1Id, Guid user2Id)
    {
        return await _dbSet
            .Include(c => c.Participants)
            .Where(c => c.Participants.Count == 2 && 
                   c.Participants.Any(p => p.UserId == user1Id) && 
                   c.Participants.Any(p => p.UserId == user2Id) &&
                   !c.IsGroup)
            .FirstOrDefaultAsync();
    }
}

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Message?> GetWithAttachmentsAsync(Guid messageId)
    {
        return await _dbSet
            .Include(m => m.Attachments)
            .FirstOrDefaultAsync(m => m.Id == messageId);
    }

    public async Task<IEnumerable<Message>> GetByConversationAsync(Guid conversationId, int skip, int take)
    {
        return await _dbSet
            .Where(m => m.ConversationId == conversationId && m.DeletedAt == null)
            .Include(m => m.Sender)
            .OrderByDescending(m => m.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(Guid conversationId, Guid userId)
    {
        return await _dbSet
            .CountAsync(m => m.ConversationId == conversationId && 
                      m.SenderId != userId && 
                      m.Status != DEPI.Domain.Entities.Messaging.MessageStatus.Read &&
                      m.DeletedAt == null);
    }
}

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId, bool unreadOnly = false)
    {
        var query = _dbSet.Where(n => n.UserId == userId);

        if (unreadOnly)
            query = query.Where(n => !n.IsRead);

        return await query
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetUnreadCountAsync(Guid userId)
    {
        return await _dbSet
            .CountAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task MarkAllAsReadAsync(Guid userId)
    {
        var notifications = await _dbSet
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();

        foreach (var notification in notifications)
        {
            notification.MarkAsRead();
        }

        await _context.SaveChangesAsync();
    }
}