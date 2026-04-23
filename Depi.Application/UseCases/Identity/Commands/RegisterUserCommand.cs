using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;
using MediatR;
using System.Security.Cryptography;
using System.Text;

namespace DEPI.Application.UseCases.Identity.Commands;

public record RegisterUserCommand(RegisterRequest Request) : IRequest<AuthResponse>;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RegisterUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var registerRequest = request.Request;

        if (await _userRepository.ExistsByEmailAsync(registerRequest.Email))
            throw new InvalidOperationException(Errors.AlreadyExists("Email"));

        var (passwordHash, passwordSalt) = HashPassword(registerRequest.Password);

        var user = User.Create(
            email: registerRequest.Email,
            passwordHash: passwordHash,
            passwordSalt: passwordSalt,
            firstName: registerRequest.FirstName,
            lastName: registerRequest.LastName,
            userType: registerRequest.UserType,
            gender: registerRequest.Gender,
            dateOfBirth: registerRequest.DateOfBirth,
            phoneNumber: registerRequest.PhoneNumber,
            countryId: registerRequest.CountryId
        );

        await _userRepository.AddAsync(user, cancellationToken);

        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);

        user.SetRefreshToken(refreshToken, expiry);
        await _userRepository.UpdateAsync(user, cancellationToken);

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.UserType.ToString());

        return new AuthResponse(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresAt: expiry,
            User: MapToUserResponse(user)
        );
    }

    private static (string Hash, string Salt) HashPassword(string password)
    {
        var salt = GenerateSalt();
        var hash = ComputeHash(password, salt);
        return (hash, salt);
    }

    private static string GenerateSalt()
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    private static string ComputeHash(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var combined = password + salt;
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
        return Convert.ToBase64String(bytes);
    }

    private static UserResponse MapToUserResponse(User user)
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