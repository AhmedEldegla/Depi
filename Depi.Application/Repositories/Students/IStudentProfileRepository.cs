using DEPI.Domain.Entities.Students;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Students;

public interface IStudentProfileRepository : IRepository<StudentProfile>
{
    Task<StudentProfile?> GetByUserIdAsync(string userId);
    Task<List<StudentProfile>> GetActiveAsync();
    Task<List<StudentProfile>> GetReadyForMarketAsync();
    Task<List<StudentProfile>> GetByCoachIdAsync(string coachId);
    Task<List<StudentProfile>> GetByStatusAsync(StudentStatus status);
    Task<List<StudentProfile>> GetTopStudentsAsync(int count);
}
