namespace DEPI.Domain.Common.Interfaces;

public interface ITokenService
{
    Task<TokenResult> GenerateAccessTokenAsync(Guid userId, string email, IEnumerable<string> roles);
    string GenerateRefreshToken();
    Task<bool> ValidateTokenAsync(string token);
    Task<Guid?> GetUserIdFromTokenAsync(string token);
    Task<string?> GetEmailFromTokenAsync(string token);
    string HashToken(string token);
    Task InvalidateRefreshTokenAsync(Guid userId, string refreshToken);
    Task<bool> ValidateRefreshTokenCachedAsync(Guid userId, string refreshToken);
}

public class TokenResult
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
}
