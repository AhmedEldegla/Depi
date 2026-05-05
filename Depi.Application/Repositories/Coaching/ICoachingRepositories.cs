using DEPI.Domain.Entities.Coaching;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Coaching;

public interface ICoachingSessionRepository : IRepository<CoachingSession>
{
    Task<List<CoachingSession>> GetByCoachIdAsync(Guid coachId);
    Task<List<CoachingSession>> GetByStudentIdAsync(Guid studentId);
    Task<List<CoachingSession>> GetUpcomingAsync(Guid userId);
    Task<List<CoachingSession>> GetScheduledAsync(Guid coachId, DateTime date);
}

public interface ICoachProfileRepository : IRepository<CoachProfile>
{
    Task<CoachProfile?> GetByUserIdAsync(Guid userId);
    Task<List<CoachProfile>> GetAvailableCoachesAsync();
    Task<List<CoachProfile>> GetTopCoachesAsync(int count);
}
