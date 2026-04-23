namespace DEPI.Application.DTOs.Identity;

public record UserSessionDto(
    Guid Id,
    Guid UserId,
    string IpAddress,
    string UserAgent,
    DateTime CreatedAt,
    DateTime? ExpiresAt,
    bool IsActive
);
