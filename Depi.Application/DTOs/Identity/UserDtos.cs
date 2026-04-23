using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Identity;

public record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    UserType UserType,
    Gender Gender,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    int? CountryId
);

public record LoginRequest(
    string Email,
    string Password,
    string? TwoFactorCode
);

public record RefreshTokenRequest(
    string RefreshToken
);

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);

public record UpdateProfileRequest(
    string FirstName,
    string LastName,
    Gender Gender,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    int? CountryId
);

public record VerifyEmailRequest(
    string Token
);

public record VerifyPhoneRequest(
    string Code
);

public record ForgotPasswordRequest(
    string Email
);

public record ResetPasswordRequest(
    string Token,
    string NewPassword
);

public record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string FullName,
    UserType UserType,
    UserStatus Status,
    Gender Gender,
    DateTime? DateOfBirth,
    string? PhoneNumber,
    string? ProfileImageUrl,
    bool IsEmailVerified,
    bool IsPhoneVerified,
    bool IsIdentityVerified,
    bool IsTwoFactorEnabled,
    DateTime? LastLoginAt,
    DateTime CreatedAt
);

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    UserResponse User
);

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry,
    DateTime RefreshTokenExpiry,
    UserResponse User
);

public record TokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);