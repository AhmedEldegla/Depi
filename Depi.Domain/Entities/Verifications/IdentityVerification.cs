namespace DEPI.Domain.Entities.Verifications;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Enums;

public class IdentityVerification : AuditableEntity
{
    public Guid UserId { get; private set; }
    public DocumentType DocumentType { get; private set; }
    public string DocumentImageUrl { get; private set; } = string.Empty;
    public string SelfieImageUrl { get; private set; } = string.Empty;
    public VerificationStatus Status { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime? ReviewedAt { get; private set; }
    public Guid? ReviewedBy { get; private set; }

    public virtual User User { get; private set; } = null!;

    private IdentityVerification() { }

    public static IdentityVerification Submit(
        Guid userId,
        DocumentType documentType,
        string documentImageUrl,
        string selfieImageUrl)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("المستخدم مطلوب");

        if (string.IsNullOrWhiteSpace(documentImageUrl))
            throw new ArgumentException("صورة وثيقة الهوية مطلوبة");

        if (string.IsNullOrWhiteSpace(selfieImageUrl))
            throw new ArgumentException("صورة السيلفي مع الوثيقة مطلوبة");

        if (documentImageUrl == selfieImageUrl)
            throw new ArgumentException("صورة الوثيقة وصورة السيلفي يجب أن تكونا مختلفتين");

        return new IdentityVerification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            DocumentType = documentType,
            DocumentImageUrl = documentImageUrl,
            SelfieImageUrl = selfieImageUrl,
            Status = VerificationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Approve(Guid reviewedBy)
    {
        if (Status != VerificationStatus.Pending)
            throw new InvalidOperationException("لا يمكن قبول طلب غير معلق");

        Status = VerificationStatus.Approved;
        ReviewedAt = DateTime.UtcNow;
        ReviewedBy = reviewedBy;
    }

    public void Reject(Guid reviewedBy, string reason)
    {
        if (Status != VerificationStatus.Pending)
            throw new InvalidOperationException("لا يمكن رفض طلب غير معلق");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("سبب الرفض مطلوب");

        Status = VerificationStatus.Rejected;
        RejectionReason = reason.Trim();
        ReviewedAt = DateTime.UtcNow;
        ReviewedBy = reviewedBy;
    }
}
