namespace DEPI.Application.UseCases.Verifications.SubmitVerification;

using DEPI.Application.Common;
using DEPI.Application.DTOs.Verifications;
using DEPI.Application.Repositories.Verifications;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Verifications;
using DEPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class SubmitIdentityVerificationCommandHandler
    : IRequestHandler<SubmitIdentityVerificationCommand, IdentityVerificationResponse>
{
    private readonly IIdentityVerificationRepository _verificationRepo;
    private readonly UserManager<User> _userManager;

    public SubmitIdentityVerificationCommandHandler(
        IIdentityVerificationRepository verificationRepo,
        UserManager<User> userManager)
    {
        _verificationRepo = verificationRepo;
        _userManager = userManager;
    }

    public async Task<IdentityVerificationResponse> Handle(
        SubmitIdentityVerificationCommand command, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        if (user == null)
            throw new InvalidOperationException("المستخدم غير موجود");

        if (user.IsIdentityVerified)
            throw new InvalidOperationException("هويتك موثقة بالفعل");

        var existing = await _verificationRepo.GetPendingByUserIdAsync(command.UserId);
        if (existing != null)
            throw new InvalidOperationException("لديك طلب توثيق قيد المراجعة بالفعل");

        var req = command.Request;

        var verification = IdentityVerification.Submit(
            command.UserId,
            req.DocumentType,
            req.DocumentImageUrl,
            req.SelfieImageUrl
        );

        await _verificationRepo.AddAsync(verification);

        return MapToResponse(verification, user);
    }

    private static IdentityVerificationResponse MapToResponse(
        IdentityVerification v, User user)
    {
        return new IdentityVerificationResponse(
            v.Id,
            v.UserId,
            user.FullName,
            v.DocumentType,
            GetDocumentTypeName(v.DocumentType),
            v.DocumentImageUrl,
            v.SelfieImageUrl,
            v.Status,
            GetStatusName(v.Status),
            v.RejectionReason,
            v.CreatedAt,
            v.ReviewedAt,
            v.ReviewedBy
        );
    }

    private static string GetDocumentTypeName(DocumentType type) => type switch
    {
        DocumentType.NationalID => "بطاقة هوية وطنية",
        DocumentType.Passport => "جواز سفر",
        DocumentType.DriversLicense => "رخصة قيادة",
        DocumentType.ResidencePermit => "إقامة",
        _ => type.ToString()
    };

    private static string GetStatusName(VerificationStatus status) => status switch
    {
        VerificationStatus.Pending => "قيد المراجعة",
        VerificationStatus.Approved => "مقبول",
        VerificationStatus.Rejected => "مرفوض",
        _ => status.ToString()
    };
}
