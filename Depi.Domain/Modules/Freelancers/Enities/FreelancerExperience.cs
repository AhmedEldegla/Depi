using Depi.Domain.Modules.Freelancer.Enums;
using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Freelancer.Entities;

public class FreelancerExperience : BaseEntity
{
    public Guid FreelancerId { get; set; }

    public ExperienceType ExperienceType { get; set; }

    [Required]
    [MaxLength(200)]
    public string OrganizationName { get; set; } = string.Empty;

    [Required]
    [MaxLength(160)]
    public string Title { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    // Navigation Properties
    public virtual Freelancer Freelancer { get; set; } = null!;
}