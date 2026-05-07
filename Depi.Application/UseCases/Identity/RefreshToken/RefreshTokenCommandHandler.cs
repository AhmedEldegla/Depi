using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace DEPI.Application.UseCases.Identity.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public RefreshTokenCommandHandler(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
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

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiry,
            User = _mapper.Map<UserResponse>(user)
        };
    }
}