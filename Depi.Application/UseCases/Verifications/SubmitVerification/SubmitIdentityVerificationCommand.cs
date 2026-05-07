using DEPI.Application.DTOs.Verifications;
using MediatR;

namespace DEPI.Application.UseCases.Verifications.SubmitVerification;

public record SubmitIdentityVerificationCommand(
    Guid UserId,
    SubmitIdentityVerificationRequest Request
) : IRequest<IdentityVerificationResponse>;
