using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace DEPI.Application.UseCases.Identity.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public LoginCommandHandler(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
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
        var userResponse = _mapper.Map<UserResponse>(user);
        userResponse.Roles = roles.ToList();


        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiry,
            User = userResponse
        };
    }
}