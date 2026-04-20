using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Freelancer.Entities;

public class PortfolioItem : BaseEntity
{
    public Guid FreelancerId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid? MediaId { get; set; }

    [MaxLength(500)]
    public string? ExternalUrl { get; set; }

    // Navigation Properties
    public virtual Freelancer Freelancer { get; set; } = null!;
}