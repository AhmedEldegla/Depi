// Identity/Register/RegisterCommandHandler.cs
using Depi.Application.Repositories.Common;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
namespace DEPI.Application.UseCases.Identity.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    public RegisterCommandHandler(UserManager<User> userManager, ITokenService tokenService) { _userManager = userManager; _tokenService = tokenService; }
    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerRequest = request.Request;
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser != null) throw new InvalidOperationException(Errors.AlreadyExists("Email"));
        var user = User.Create(registerRequest.Email, registerRequest.Email, registerRequest.FirstName, registerRequest.LastName, registerRequest.UserType, registerRequest.Gender, registerRequest.DateOfBirth, registerRequest.PhoneNumber, registerRequest.CountryId);
        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded) throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);
        user.SetRefreshToken(refreshToken, expiry);
        await _userManager.UpdateAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email ?? "", user.UserType.ToString());
        return new AuthResponse(AccessToken: accessToken, RefreshToken: refreshToken, ExpiresAt: expiry, User: MapToUserResponse(user));
    }
    private static UserResponse MapToUserResponse(User user) => new UserResponse(Id: user.Id, Email: user.Email ?? "", FirstName: user.FirstName, LastName: user.LastName, FullName: user.FullName, UserType: user.UserType, Status: user.Status, Gender: user.Gender, DateOfBirth: user.DateOfBirth, PhoneNumber: user.PhoneNumber, ProfileImageUrl: user.ProfileImageUrl, IsEmailVerified: user.EmailConfirmed, IsPhoneVerified: user.PhoneNumberConfirmed, IsIdentityVerified: user.IsIdentityVerified, IsTwoFactorEnabled: user.TwoFactorEnabled, LastLoginAt: user.LastLoginAt, CreatedAt: user.CreatedAt);
}