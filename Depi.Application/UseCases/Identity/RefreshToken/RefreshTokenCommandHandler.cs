using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Depi.Application.Repositories.Common;
namespace DEPI.Application.UseCases.Identity.RefreshToken;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public RefreshTokenCommandHandler(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == request.RefreshToken);
        if (user == null)
            throw new InvalidOperationException("رمز التحديث غير صالح");
        if (user.RefreshTokenExpiry == null || user.RefreshTokenExpiry <= DateTime.UtcNow)
            throw new InvalidOperationException("انتهت صلاحية رمز التحديث");
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);
        user.SetRefreshToken(refreshToken, expiry);
        await _userManager.UpdateAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email ?? "", string.Join(",", roles));
        return new AuthResponse(AccessToken: accessToken, RefreshToken: refreshToken, ExpiresAt: expiry, User: MapToUserResponse(user));
    }
    private static UserResponse MapToUserResponse(User user) => new UserResponse(Id: user.Id, Email: user.Email ?? "", FirstName: user.FirstName, LastName: user.LastName, FullName: user.FullName, UserType: user.UserType, Status: user.Status, Gender: user.Gender, DateOfBirth: user.DateOfBirth, PhoneNumber: user.PhoneNumber, ProfileImageUrl: user.ProfileImageUrl, IsEmailVerified: user.EmailConfirmed, IsPhoneVerified: user.PhoneNumberConfirmed, IsIdentityVerified: user.IsIdentityVerified, IsTwoFactorEnabled: user.TwoFactorEnabled, LastLoginAt: user.LastLoginAt, CreatedAt: user.CreatedAt);
}