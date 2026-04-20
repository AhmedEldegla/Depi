using System.ComponentModel.DataAnnotations;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Modules.Identity.Enums;

namespace DEPI.Domain.Modules.Identity.Entities;

public class IdentityVerification : AuditableEntity
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DocumentType DocumentType { get; set; }

    [Required]
    [MaxLength(50)]
    public string DocumentNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? FrontImageUrl { get; set; }

    [MaxLength(500)]
    public string? BackImageUrl { get; set; }

    [MaxLength(500)]
    public string? SelfieImageUrl { get; set; }

    [MaxLength(500)]
    public string? HolderFullName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [MaxLength(200)]
    public string? IssuingCountry { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public VerificationStatus Status { get; set; } = VerificationStatus.Pending;

    [MaxLength(1000)]
    public string? RejectionReason { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public Guid? ReviewedByUserId { get; set; }

    public  User? User { get; set; }

    public  User? ReviewedByUser { get; set; }
}
