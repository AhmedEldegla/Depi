using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;
using MediatR;

namespace DEPI.Application.UseCases.Identity.Commands;

public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = request.RefreshToken;

        var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);

        if (user == null)
            throw new InvalidOperationException(Errors.Unauthorized());

        if (user.RefreshTokenExpiry.HasValue && user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            user.InvalidateRefreshToken();
            await _userRepository.UpdateAsync(user, cancellationToken);
            throw new InvalidOperationException("Refresh token expired");
        }

        if (user.Status == UserStatus.Suspended)
            throw new InvalidOperationException("Account is suspended");

        if (user.Status == UserStatus.Banned)
            throw new InvalidOperationException("Account is banned");

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.UserType.ToString());
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);

        user.SetRefreshToken(newRefreshToken, expiry);
        await _userRepository.UpdateAsync(user, cancellationToken);

        return new AuthResponse(
            AccessToken: accessToken,
            RefreshToken: newRefreshToken,
            ExpiresAt: expiry,
            User: MapToUserResponse(user)
        );
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