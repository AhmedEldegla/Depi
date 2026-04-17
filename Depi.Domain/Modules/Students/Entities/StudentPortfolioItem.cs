using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Students.Entities;

public class StudentPortfolioItem : BaseEntity
{
    public Guid StudentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [MaxLength(500)]
    public string? ExternalUrl { get; set; }

    public Guid? MediaId { get; set; }

    // Navigation Properties
    public virtual Student Student { get; set; } = null!;
}