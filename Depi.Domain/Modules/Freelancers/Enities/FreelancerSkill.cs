using Depi.Domain.Modules.Freelancer.Enums;
using DEPI.Domain.Common.Base;

namespace Depi.Domain.Modules.Freelancer.Entities;

public class FreelancerSkill : BaseEntity
{
    public Guid FreelancerId { get; set; }
    public Guid SkillId { get; set; }

    public SkillLevel Level { get; set; }

    public int? YearsExperience { get; set; }

    // Navigation Properties
    public virtual Freelancer Freelancer { get; set; } = null!;
    public virtual Skill Skill { get; set; } = null!;
}