namespace DEPI.Application.UseCases.Verifications.GetVerifications;

using DEPI.Application.DTOs.Verifications;
using DEPI.Application.Repositories.Verifications;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class GetMyVerificationsQueryHandler
    : IRequestHandler<GetMyVerificationsQuery, List<IdentityVerificationResponse>>
{
    private readonly IIdentityVerificationRepository _verificationRepo;
    private readonly UserManager<User> _userManager;

    public GetMyVerificationsQueryHandler(
        IIdentityVerificationRepository verificationRepo,
        UserManager<User> userManager)
    {
        _verificationRepo = verificationRepo;
        _userManager = userManager;
    }

    public async Task<List<IdentityVerificationResponse>> Handle(
        GetMyVerificationsQuery query, CancellationToken ct)
    {
        var verifications = await _verificationRepo.GetByUserIdAsync(query.UserId);
        return verifications.Select(v => Map(v, null)).ToList();
    }

    private static IdentityVerificationResponse Map(
        Domain.Entities.Verifications.IdentityVerification v, User? user)
    {
        return new IdentityVerificationResponse(
            v.Id, v.UserId, user?.FullName ?? "",
            v.DocumentType, v.DocumentType switch
            {
                DocumentType.NationalID => "بطاقة هوية وطنية",
                DocumentType.Passport => "جواز سفر",
                DocumentType.DriversLicense => "رخصة قيادة",
                DocumentType.ResidencePermit => "إقامة",
                _ => v.DocumentType.ToString()
            },
            v.DocumentImageUrl, v.SelfieImageUrl,
            v.Status, v.Status switch
            {
                VerificationStatus.Pending => "قيد المراجعة",
                VerificationStatus.Approved => "مقبول",
                VerificationStatus.Rejected => "مرفوض",
                _ => v.Status.ToString()
            },
            v.RejectionReason, v.CreatedAt,
            v.ReviewedAt, v.ReviewedBy
        );
    }
}

public class GetPendingVerificationsQueryHandler
    : IRequestHandler<GetPendingVerificationsQuery, List<IdentityVerificationResponse>>
{
    private readonly IIdentityVerificationRepository _verificationRepo;

    public GetPendingVerificationsQueryHandler(
        IIdentityVerificationRepository verificationRepo)
    {
        _verificationRepo = verificationRepo;
    }

    public async Task<List<IdentityVerificationResponse>> Handle(
        GetPendingVerificationsQuery query, CancellationToken ct)
    {
        var verifications = await _verificationRepo.GetPendingVerificationsAsync();
        return verifications.Select(v => new IdentityVerificationResponse(
            v.Id, v.UserId, v.User?.FullName ?? "",
            v.DocumentType, v.DocumentType switch
            {
                DocumentType.NationalID => "بطاقة هوية وطنية",
                DocumentType.Passport => "جواز سفر",
                DocumentType.DriversLicense => "رخصة قيادة",
                DocumentType.ResidencePermit => "إقامة",
                _ => v.DocumentType.ToString()
            },
            v.DocumentImageUrl, v.SelfieImageUrl,
            v.Status, v.Status switch
            {
                VerificationStatus.Pending => "قيد المراجعة",
                VerificationStatus.Approved => "مقبول",
                VerificationStatus.Rejected => "مرفوض",
                _ => v.Status.ToString()
            },
            v.RejectionReason, v.CreatedAt,
            v.ReviewedAt, v.ReviewedBy
        )).ToList();
    }
}
