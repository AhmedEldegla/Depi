using Depi.Domain.Modules.Guild.Enums;
using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Guild.Entities;

public class Guild : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [MaxLength(500)]
    public string? IconUrl { get; set; }

    [MaxLength(500)]
    public string? BannerUrl { get; set; }

    public Guid CreatedById { get; set; }

    public int MemberCount { get; set; }

    public GuildStatus Status { get; set; }

    // Navigation Properties
    public virtual ICollection<GuildMember> Members { get; set; } = new List<GuildMember>();
    public virtual ICollection<GuildSkill> RequiredSkills { get; set; } = new List<GuildSkill>();
    public virtual ICollection<GuildSkill> Tasks { get; set; } = new List<GuildSkill>();
}