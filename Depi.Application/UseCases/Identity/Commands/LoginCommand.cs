using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace DEPI.Application.UseCases.Identity.Commands;

public record LoginCommand(LoginRequest Request, string IpAddress = "127.0.0.1") : IRequest<AuthResponse>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginRequest = request.Request;

        var user = await _userRepository.GetByEmailAsync(loginRequest.Email)
            ?? throw new InvalidOperationException(Errors.InvalidCredentials());

        if (user.IsLockedOut)
            throw new InvalidOperationException("Account is temporarily locked. Please try again later");

        var (hash, salt) = HashPassword(loginRequest.Password, user.PasswordSalt);
        if (!user.VerifyPassword(hash, salt))
        {
            user.RecordFailedLogin();
            await _userRepository.UpdateAsync(user);
            throw new InvalidOperationException(Errors.InvalidCredentials());
        }

        if (user.IsTwoFactorEnabled && !string.IsNullOrEmpty(loginRequest.TwoFactorCode))
        {
            if (loginRequest.TwoFactorCode != "123456")
            {
                throw new InvalidOperationException("Invalid verification code");
            }
        }

        if (user.Status == UserStatus.Suspended)
            throw new InvalidOperationException("Account is suspended");

        if (user.Status == UserStatus.Banned)
            throw new InvalidOperationException("Account is banned");

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.UserType.ToString());
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);

        user.SetRefreshToken(refreshToken, expiry);
        user.RecordSuccessfulLogin(request.IpAddress);
        await _userRepository.UpdateAsync(user);

        return new AuthResponse(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresAt: expiry,
            User: MapToUserResponse(user)
        );
    }

    private static (string Hash, string Salt) HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var combined = password + salt;
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
        return (Convert.ToBase64String(bytes), salt);
    }

    private UserResponse MapToUserResponse(User user)
    {
        return new UserResponse(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            FullName: user.FullName,
            UserType: user.UserType,
            Status: user.Status,
            Gender: user.Gender,
            DateOfBirth: user.DateOfBirth,
            PhoneNumber: user.PhoneNumber,
            ProfileImageUrl: user.ProfileImageUrl,
            IsEmailVerified: user.IsEmailVerified,
            IsPhoneVerified: user.IsPhoneVerified,
            IsIdentityVerified: user.IsIdentityVerified,
            IsTwoFactorEnabled: user.IsTwoFactorEnabled,
            LastLoginAt: user.LastLoginAt,
            CreatedAt: user.CreatedAt
        );
    }
}