namespace DEPI.Application.DTOs.Verifications;

using DEPI.Domain.Enums;

public record SubmitIdentityVerificationRequest(
    DocumentType DocumentType,
    string DocumentImageUrl,
    string SelfieImageUrl
);

public record ReviewVerificationRequest(
    string? RejectionReason
);

public record IdentityVerificationResponse(
    Guid Id,
    Guid UserId,
    string UserName,
    DocumentType DocumentType,
    string DocumentTypeName,
    string DocumentImageUrl,
    string SelfieImageUrl,
    VerificationStatus Status,
    string StatusName,
    string? RejectionReason,
    DateTime SubmittedAt,
    DateTime? ReviewedAt,
    Guid? ReviewedBy
);
