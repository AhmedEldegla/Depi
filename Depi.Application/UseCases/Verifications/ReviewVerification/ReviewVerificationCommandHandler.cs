namespace DEPI.Application.UseCases.Verifications.ReviewVerification;

using DEPI.Application.Common;
using DEPI.Application.DTOs.Verifications;
using DEPI.Application.Repositories.Verifications;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class ApproveVerificationCommandHandler
    : IRequestHandler<ApproveVerificationCommand, IdentityVerificationResponse>
{
    private readonly IIdentityVerificationRepository _verificationRepo;
    private readonly UserManager<User> _userManager;

    public ApproveVerificationCommandHandler(
        IIdentityVerificationRepository verificationRepo,
        UserManager<User> userManager)
    {
        _verificationRepo = verificationRepo;
        _userManager = userManager;
    }

    public async Task<IdentityVerificationResponse> Handle(
        ApproveVerificationCommand command, CancellationToken ct)
    {
        var verification = await _verificationRepo.GetByIdAsync(command.VerificationId);
        if (verification == null)
            throw new InvalidOperationException("طلب التوثيق غير موجود");

        verification.Approve(command.AdminId);
        await _verificationRepo.UpdateAsync(verification);

        var user = await _userManager.FindByIdAsync(verification.UserId.ToString());
        if (user != null)
        {
            user.VerifyIdentity();
            await _userManager.UpdateAsync(user);
        }

        return MapToResponse(verification, user);
    }

    private static IdentityVerificationResponse MapToResponse(
        Domain.Entities.Verifications.IdentityVerification v, User? user)
    {
        return new IdentityVerificationResponse(
            v.Id, v.UserId, user?.FullName ?? "",
            v.DocumentType, GetDocType(v.DocumentType),
            v.DocumentImageUrl, v.SelfieImageUrl,
            v.Status, GetStatus(v.Status),
            v.RejectionReason, v.CreatedAt, v.ReviewedAt, v.ReviewedBy
        );
    }

    private static string GetDocType(DocumentType t) => t switch
    {
        DocumentType.NationalID => "بطاقة هوية وطنية",
        DocumentType.Passport => "جواز سفر",
        DocumentType.DriversLicense => "رخصة قيادة",
        DocumentType.ResidencePermit => "إقامة",
        _ => t.ToString()
    };

    private static string GetStatus(VerificationStatus s) => s switch
    {
        VerificationStatus.Pending => "قيد المراجعة",
        VerificationStatus.Approved => "مقبول",
        VerificationStatus.Rejected => "مرفوض",
        _ => s.ToString()
    };
}

public class RejectVerificationCommandHandler
    : IRequestHandler<RejectVerificationCommand, IdentityVerificationResponse>
{
    private readonly IIdentityVerificationRepository _verificationRepo;
    private readonly UserManager<User> _userManager;

    public RejectVerificationCommandHandler(
        IIdentityVerificationRepository verificationRepo,
        UserManager<User> userManager)
    {
        _verificationRepo = verificationRepo;
        _userManager = userManager;
    }

    public async Task<IdentityVerificationResponse> Handle(
        RejectVerificationCommand command, CancellationToken ct)
    {
        var verification = await _verificationRepo.GetByIdAsync(command.VerificationId);
        if (verification == null)
            throw new InvalidOperationException("طلب التوثيق غير موجود");

        verification.Reject(command.AdminId, command.Reason);
        await _verificationRepo.UpdateAsync(verification);

        var user = await _userManager.FindByIdAsync(verification.UserId.ToString());

        return new IdentityVerificationResponse(
            verification.Id, verification.UserId, user?.FullName ?? "",
            verification.DocumentType, verification.DocumentType switch
            {
                DocumentType.NationalID => "بطاقة هوية وطنية",
                DocumentType.Passport => "جواز سفر",
                DocumentType.DriversLicense => "رخصة قيادة",
                DocumentType.ResidencePermit => "إقامة",
                _ => verification.DocumentType.ToString()
            },
            verification.DocumentImageUrl, verification.SelfieImageUrl,
            verification.Status, verification.Status switch
            {
                VerificationStatus.Pending => "قيد المراجعة",
                VerificationStatus.Approved => "مقبول",
                VerificationStatus.Rejected => "مرفوض",
                _ => verification.Status.ToString()
            },
            verification.RejectionReason, verification.CreatedAt,
            verification.ReviewedAt, verification.ReviewedBy
        );
    }
}
