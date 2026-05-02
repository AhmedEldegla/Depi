using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Identity;
using DEPI.Application.Interfaces;
using DEPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DEPI.Application.UseCases.Identity.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerRequest = request.Request;
        var existingUser = await _userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser != null) throw new InvalidOperationException(Errors.AlreadyExists("Email"));

        var user = User.Create(
            registerRequest.Email,
            registerRequest.Email,
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.UserType,
            registerRequest.Gender,
            registerRequest.DateOfBirth,
            registerRequest.PhoneNumber,
            registerRequest.CountryId);

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded) throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));

        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);
        user.SetRefreshToken(refreshToken, expiry);
        await _userManager.UpdateAsync(user);

        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email ?? "", user.UserType.ToString());

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiry,
            User = _mapper.Map<UserResponse>(user)
        };
    }
}