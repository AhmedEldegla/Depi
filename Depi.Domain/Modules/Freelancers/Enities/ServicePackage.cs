using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Freelancer.Entities;

public class ServicePackage : BaseEntity
{
    public Guid FreelancerId { get; set; }

    public int? CategoryId { get; set; }

    [Required]
    [MaxLength(180)]
    public string Title { get; set; } = string.Empty;

    public decimal BasePrice { get; set; }

    public int DeliveryDays { get; set; }

    // Navigation Properties
    public virtual Freelancer Freelancer { get; set; } = null!;
}