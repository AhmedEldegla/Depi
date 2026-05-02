using DEPI.Application.Repositories.Students;
using DEPI.Domain.Entities.Students;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class StudentProfileRepository : Repository<StudentProfile>, IStudentProfileRepository
{
    public StudentProfileRepository(ApplicationDbContext context) : base(context) { }

    public async Task<StudentProfile?> GetByUserIdAsync(string userId)
        => await _dbSet.FirstOrDefaultAsync(s => s.UserId == userId);

    public async Task<List<StudentProfile>> GetActiveAsync()
        => await _dbSet.Where(s => s.Status == StudentStatus.Active).OrderByDescending(s => s.ReadinessScore).ToListAsync();

    public async Task<List<StudentProfile>> GetReadyForMarketAsync()
        => await _dbSet.Where(s => s.IsReadyForMarket).OrderByDescending(s => s.ReadinessScore).ToListAsync();

    public async Task<List<StudentProfile>> GetByCoachIdAsync(string coachId)
        => await _dbSet.Where(s => s.AssignedCoachId == coachId).OrderByDescending(s => s.ReadinessScore).ToListAsync();

    public async Task<List<StudentProfile>> GetByStatusAsync(StudentStatus status)
        => await _dbSet.Where(s => s.Status == status).ToListAsync();

    public async Task<List<StudentProfile>> GetTopStudentsAsync(int count)
        => await _dbSet.OrderByDescending(s => s.ReadinessScore).Take(count).ToListAsync();
}
