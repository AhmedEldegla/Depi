using DEPI.Domain.Entities.HeadHunters;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.HeadHunters;

public interface IHeadHunterRepository : IRepository<HeadHunter>
{
    Task<HeadHunter?> GetByUserIdAsync(string userId);
    Task<List<HeadHunter>> GetActiveAsync();
    Task<List<HeadHunter>> GetBySpecializationAsync(string specialization);
    Task<List<HeadHunter>> GetTopPerformersAsync(int count);
}

public interface IHeadHunterAssignmentRepository : IRepository<HeadHunterAssignment>
{
    Task<List<HeadHunterAssignment>> GetByHunterIdAsync(Guid hunterId);
    Task<List<HeadHunterAssignment>> GetActiveAssignmentsAsync();
    Task<List<HeadHunterAssignment>> GetByClientIdAsync(string clientId);
    Task<HeadHunterAssignment?> GetActiveAssignmentAsync(Guid hunterId, string clientId);
}

public interface ITalentRecommendationRepository : IRepository<TalentRecommendation>
{
    Task<List<TalentRecommendation>> GetByAssignmentIdAsync(Guid assignmentId);
    Task<List<TalentRecommendation>> GetByHunterIdAsync(Guid hunterId);
    Task<List<TalentRecommendation>> GetPendingAsync(Guid assignmentId);
    Task<List<TalentRecommendation>> GetByUserIdAsync(string userId);
}
