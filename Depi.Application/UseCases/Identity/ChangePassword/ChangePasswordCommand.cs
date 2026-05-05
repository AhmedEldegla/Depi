using DEPI.Application.DTOs.Identity;
using MediatR;

namespace DEPI.Application.UseCases.Identity.ChangePassword;

public record ChangePasswordCommand(Guid UserId, ChangePasswordRequest Request) : IRequest;
