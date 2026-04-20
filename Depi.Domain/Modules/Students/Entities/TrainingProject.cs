using Depi.Domain.Modules.Students.Enums;
using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Students.Entities;

public class TrainingProject : BaseEntity
{
    public Guid StudentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [MaxLength(500)]
    public string? RepoUrl { get; set; }

    [MaxLength(500)]
    public string? LiveUrl { get; set; }

    // JSON: ["React", "Node.js"]
    public string? Technologies { get; set; }

    public TrainingProjectStatus Status { get; set; }

    // Navigation Properties
    public virtual Student Student { get; set; } = null!;
}