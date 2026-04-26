using DEPI.Application.DTOs.Identity;
using MediatR;
namespace DEPI.Application.UseCases.Identity.Login;
public record LoginCommand(LoginRequest Request, string IpAddress = "127.0.0.1") : IRequest<AuthResponse>;