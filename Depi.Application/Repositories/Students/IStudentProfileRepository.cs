using DEPI.Domain.Entities.Students;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Students;

public interface IStudentProfileRepository : IRepository<StudentProfile>
{
    Task<StudentProfile?> GetByUserIdAsync(Guid userId);
    Task<List<StudentProfile>> GetActiveAsync();
    Task<List<StudentProfile>> GetReadyForMarketAsync();
    Task<List<StudentProfile>> GetByCoachIdAsync(Guid coachId);
    Task<List<StudentProfile>> GetByStatusAsync(StudentStatus status);
    Task<List<StudentProfile>> GetTopStudentsAsync(int count);
}
