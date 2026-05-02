using DEPI.Domain.Entities.Coaching;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Coaching;

public interface ICoachingSessionRepository : IRepository<CoachingSession>
{
    Task<List<CoachingSession>> GetByCoachIdAsync(string coachId);
    Task<List<CoachingSession>> GetByStudentIdAsync(string studentId);
    Task<List<CoachingSession>> GetUpcomingAsync(string userId);
    Task<List<CoachingSession>> GetScheduledAsync(string coachId, DateTime date);
}

public interface ICoachProfileRepository : IRepository<CoachProfile>
{
    Task<CoachProfile?> GetByUserIdAsync(string userId);
    Task<List<CoachProfile>> GetAvailableCoachesAsync();
    Task<List<CoachProfile>> GetTopCoachesAsync(int count);
}
