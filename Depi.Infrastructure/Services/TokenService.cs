using Depi.Application.Repositories.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DEPI.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly string _jwtSecret;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;
    private readonly int _accessTokenExpirationMinutes = 15;
    private readonly int _refreshTokenExpirationDays = 7;

    public TokenService(IConfiguration configuration)
    {
        _jwtSecret = configuration["Jwt:SecretKey"] ?? "DEPI_SmartFreelance_Secret_Key_For_Development_2026_This_Should_Be_In_Production_Settings";
        _jwtIssuer = configuration["Jwt:Issuer"] ?? "DEPI.SmartFreelance";
        _jwtAudience = configuration["Jwt:Audience"] ?? "DEPI.SmartFreelance.API";
    }

    public string GenerateAccessToken(Guid userId, string email, string userType)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("type", userType)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        var token = Convert.ToBase64String(randomNumber);
        return token;
    }

    public (Guid UserId, string Email, string UserType)? ValidateAccessToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _jwtIssuer,
                ValidateAudience = true,
                ValidAudience = _jwtAudience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = handler.ValidateToken(token, validationParameters, out _);

            var userIdClaim = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var emailClaim = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            var typeClaim = principal.FindFirst("type")?.Value;

            if (userIdClaim == null || emailClaim == null || typeClaim == null)
                return null;

            return (
                UserId: Guid.Parse(userIdClaim),
                Email: emailClaim,
                UserType: typeClaim
            );
        }
        catch
        {
            return null;
        }
    }

    public Guid? ValidateRefreshToken(string token)
    {
        return null;
    }

    public DateTime? GetRefreshTokenExpiry(string token)
    {
        return DateTime.UtcNow.AddDays(_refreshTokenExpirationDays);
    }
}