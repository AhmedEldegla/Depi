namespace DEPI.Application.Repositories.Verifications;

using DEPI.Domain.Entities.Verifications;

public interface IIdentityVerificationRepository
{
    Task<IdentityVerification?> GetByIdAsync(Guid id);
    Task<IdentityVerification?> GetPendingByUserIdAsync(Guid userId);
    Task<List<IdentityVerification>> GetByUserIdAsync(Guid userId);
    Task<List<IdentityVerification>> GetPendingVerificationsAsync();
    Task<IdentityVerification> AddAsync(IdentityVerification verification);
    Task UpdateAsync(IdentityVerification verification);
    Task<bool> HasExistingVerificationAsync(Guid userId);
}
