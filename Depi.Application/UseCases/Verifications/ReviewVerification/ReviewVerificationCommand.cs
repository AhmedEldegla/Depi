using DEPI.Application.DTOs.Verifications;
using MediatR;

namespace DEPI.Application.UseCases.Verifications.ReviewVerification;

public record ApproveVerificationCommand(Guid VerificationId, Guid AdminId) : IRequest<IdentityVerificationResponse>;

public record RejectVerificationCommand(Guid VerificationId, Guid AdminId, string Reason) : IRequest<IdentityVerificationResponse>;
