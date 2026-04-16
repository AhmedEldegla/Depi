using Depi.Domain.Modules.Guild.Enums;
using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;
namespace Depi.Domain.Modules.Guild.Entities;

using Depi.Domain.Modules.Guild.Enums;
public class TeamTask : BaseEntity
{
    public Guid GuildId { get; set; }

    public Guid? AssignedToId { get; set; }

    public Guid? CreatedById { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TaskStatus Status { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }

    // Navigation Properties
    public virtual Guild Guild { get; set; } = null!;
}