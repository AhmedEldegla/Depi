using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Enums;

namespace DEPI.Domain.Modules.Identity.Entities;

public class User : AuditableEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public int? CountryId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsEmailVerified { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    public ICollection<SecurityLog> SecurityLogs { get; set; } = new HashSet<SecurityLog>();
    public ICollection<Token> Tokens { get; set; } = new HashSet<Token>();

}
