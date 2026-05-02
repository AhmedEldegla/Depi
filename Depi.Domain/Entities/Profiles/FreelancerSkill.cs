namespace DEPI.Domain.Entities.Profiles;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class FreelancerSkill : AuditableEntity
{
    public Guid UserId { get; private set; }
    public Guid SkillId { get; private set; }
    public int ProficiencyLevel { get; private set; }
    public int YearsOfExperience { get; private set; }
    public bool IsVerified { get; private set; }
    public string? VerificationProof { get; private set; }

    public virtual User? User { get; private set; }
    public virtual Skill? Skill { get; private set; }

    private FreelancerSkill() { }

    public static FreelancerSkill Create(
        Guid userId,
        Guid skillId,
        int proficiencyLevel,
        int yearsOfExperience)
    {
        return new FreelancerSkill
        {
            UserId = userId,
            SkillId = skillId,
            ProficiencyLevel = Math.Clamp(proficiencyLevel, 1, 5),
            YearsOfExperience = Math.Clamp(yearsOfExperience, 0, 50),
            IsVerified = false
        };
    }

    public void UpdateProficiency(int level, int years)
    {
        ProficiencyLevel = Math.Clamp(level, 1, 5);
        YearsOfExperience = Math.Clamp(years, 0, 50);
    }
}