using DEPI.Application.DTOs.Identity;
using MediatR;
namespace DEPI.Application.UseCases.Identity.RefreshToken;
public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;