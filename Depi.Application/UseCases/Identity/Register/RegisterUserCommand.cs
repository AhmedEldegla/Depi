using DEPI.Application.DTOs.Identity;
using MediatR;
namespace DEPI.Application.UseCases.Identity.Register;
public record RegisterUserCommand(RegisterRequest Request) : IRequest<AuthResponse>;