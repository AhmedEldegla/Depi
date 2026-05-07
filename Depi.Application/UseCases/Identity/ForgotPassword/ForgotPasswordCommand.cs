using DEPI.Application.DTOs.Identity;
using MediatR;

namespace DEPI.Application.UseCases.Identity.ForgotPassword;

public record ForgotPasswordCommand(ForgotPasswordRequest Request) : IRequest;
