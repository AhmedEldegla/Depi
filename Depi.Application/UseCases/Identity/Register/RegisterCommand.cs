// Identity/Register/RegisterCommand.cs
using DEPI.Application.DTOs.Identity;
using MediatR;
namespace DEPI.Application.UseCases.Identity.Register;
public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponse>;