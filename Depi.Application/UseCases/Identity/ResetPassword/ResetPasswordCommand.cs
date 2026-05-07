using DEPI.Application.DTOs.Identity;
using MediatR;

namespace DEPI.Application.UseCases.Identity.ResetPassword;

public record ResetPasswordCommand(ResetPasswordRequest Request) : IRequest;
