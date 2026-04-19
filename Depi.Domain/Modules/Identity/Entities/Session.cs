namespace DEPI.Domain.Modules.Identity.Entities;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class Session : BaseEntity
{
    public Guid UserId { get; set; }
    public string RefreshTokenHash { get; set; } = string.Empty;
    public string? DeviceFingerprint { get; set; }
    public string? IpAddress { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public User? User { get; set; }
}
