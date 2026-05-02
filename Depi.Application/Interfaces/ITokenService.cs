namespace DEPI.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(Guid userId, string email, string userType);
    string GenerateRefreshToken();
    (Guid UserId, string Email, string UserType)? ValidateAccessToken(string token);
    Guid? ValidateRefreshToken(string token);
    DateTime? GetRefreshTokenExpiry(string token);
}