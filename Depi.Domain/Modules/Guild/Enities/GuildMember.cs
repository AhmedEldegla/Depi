using Depi.Domain.Modules.Guild.Enums;
using DEPI.Domain.Common.Base;

namespace Depi.Domain.Modules.Guild.Entities;

public class GuildMember : BaseEntity
{
    public Guid GuildId { get; set; }

    public Guid UserId { get; set; }

    public GuildRole Role { get; set; }

    public DateTime JoinedAt { get; set; }

    public bool IsActive { get; set; }

    // Navigation Properties
    public virtual Guild Guild { get; set; } = null!;
}