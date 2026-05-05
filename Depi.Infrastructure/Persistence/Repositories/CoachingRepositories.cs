using DEPI.Application.Repositories.Coaching;
using DEPI.Domain.Entities.Coaching;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class CoachingSessionRepository : Repository<CoachingSession>, ICoachingSessionRepository
{
    public CoachingSessionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CoachingSession>> GetByCoachIdAsync(Guid coachId)
        => await _dbSet.Where(s => s.CoachId == coachId).OrderByDescending(s => s.ScheduledAt).ToListAsync();

    public async Task<List<CoachingSession>> GetByStudentIdAsync(Guid studentId)
        => await _dbSet.Where(s => s.StudentId == studentId).OrderByDescending(s => s.ScheduledAt).ToListAsync();

    public async Task<List<CoachingSession>> GetUpcomingAsync(Guid userId)
        => await _dbSet.Where(s => (s.CoachId == userId || s.StudentId == userId) && s.Status == SessionStatus.Scheduled && s.ScheduledAt > DateTime.UtcNow).OrderBy(s => s.ScheduledAt).ToListAsync();

    public async Task<List<CoachingSession>> GetScheduledAsync(Guid coachId, DateTime date)
        => await _dbSet.Where(s => s.CoachId == coachId && s.ScheduledAt.Date == date.Date && s.Status == SessionStatus.Scheduled).ToListAsync();
}

public class CoachProfileRepository : Repository<CoachProfile>, ICoachProfileRepository
{
    public CoachProfileRepository(ApplicationDbContext context) : base(context) { }

    public async Task<CoachProfile?> GetByUserIdAsync(Guid userId)
        => await _dbSet.FirstOrDefaultAsync(c => c.UserId == userId);

    public async Task<List<CoachProfile>> GetAvailableCoachesAsync()
        => await _dbSet.Where(c => c.IsAvailable).OrderByDescending(c => c.AverageRating).ToListAsync();

    public async Task<List<CoachProfile>> GetTopCoachesAsync(int count)
        => await _dbSet.Where(c => c.IsAvailable).OrderByDescending(c => c.AverageRating).Take(count).ToListAsync();
}
