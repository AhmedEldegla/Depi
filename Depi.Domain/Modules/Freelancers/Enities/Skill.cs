using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Freelancer.Entities;

public class Skill : BaseEntity
{
    [Required]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    public bool IsVerifiedSkill { get; set; }

    // Navigation Properties
    public virtual ICollection<FreelancerSkill> FreelancerSkills { get; set; } = new List<FreelancerSkill>();
}