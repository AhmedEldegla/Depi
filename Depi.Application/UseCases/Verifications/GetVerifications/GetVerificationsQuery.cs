using DEPI.Application.DTOs.Verifications;
using MediatR;

namespace DEPI.Application.UseCases.Verifications.GetVerifications;

public record GetMyVerificationsQuery(Guid UserId) : IRequest<List<IdentityVerificationResponse>>;

public record GetPendingVerificationsQuery : IRequest<List<IdentityVerificationResponse>>;
