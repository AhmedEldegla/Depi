namespace DEPI.Domain.Entities.Identity;

using DEPI.Domain.Common.Base;

public class UserSession : AuditableEntity
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public string RefreshToken { get; private set; } = string.Empty;
    public string IpAddress { get; private set; } = string.Empty;
    public string UserAgent { get; private set; } = string.Empty;
    public string DeviceType { get; private set; } = string.Empty;
    public string Browser { get; private set; } = string.Empty;
    public string Os { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public DateTime? ExpiresAt { get; private set; }

    public virtual User? User { get; private set; }

    private UserSession() { }

    public static UserSession Create(
        Guid userId,
        string token,
        string refreshToken,
        string ipAddress,
        string userAgent,
        DateTime expiresAt)
    {
        return new UserSession
        {
            UserId = userId,
            Token = token,
            RefreshToken = refreshToken,
            IpAddress = ipAddress,
            UserAgent = userAgent ?? string.Empty,
            ExpiresAt = expiresAt
        };
    }

    public void Revoke()
    {
        IsActive = false;
    }

    public bool IsExpired => ExpiresAt.HasValue && ExpiresAt < DateTime.UtcNow;
}