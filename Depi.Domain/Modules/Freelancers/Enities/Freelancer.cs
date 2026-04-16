using Depi.Domain.Modules.Freelancer.Enums;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Modules.Identity.Entities;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Freelancer.Entities;

public class Freelancer : AuditableEntity
{
    public Guid UserId { get; set; }

    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    public decimal? HourlyRate { get; set; }

    public AvailabilityStatus AvailabilityStatus { get; set; }

    public decimal? RatingAverage { get; set; }

    // Navigation Properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<FreelancerSkill> FreelancerSkills { get; set; } = new List<FreelancerSkill>();
    public virtual ICollection<PortfolioItem> PortfolioItems { get; set; } = new List<PortfolioItem>();
    public virtual ICollection<FreelancerExperience> Experiences { get; set; } = new List<FreelancerExperience>();
    public virtual ICollection<ServicePackage> ServicePackages { get; set; } = new List<ServicePackage>();
}