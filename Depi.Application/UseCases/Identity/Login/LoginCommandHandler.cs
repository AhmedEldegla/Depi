using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Depi.Application.Repositories.Common;
namespace DEPI.Application.UseCases.Identity.Login;
public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginRequest = request.Request;
        var user = await _userManager.FindByEmailAsync(loginRequest.Email) ?? throw new InvalidOperationException(Errors.InvalidCredentials());
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: true);
        if (result.IsLockedOut) throw new InvalidOperationException("الحساب مقفل");
        if (!result.Succeeded) throw new InvalidOperationException(Errors.InvalidCredentials());
        user.UpdateLastLogin(request.IpAddress);
        await _userManager.UpdateAsync(user);
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