using DEPI.Domain.Common.Base;

namespace Depi.Domain.Modules.Guild.Entities;

public class GuildSkill : BaseEntity
{
    public Guid GuildId { get; set; }

    public Guid SkillId { get; set; }

    // Navigation Properties
    public virtual Guild Guild { get; set; } = null!;
}