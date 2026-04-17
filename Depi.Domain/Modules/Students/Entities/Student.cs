using Depi.Domain.Modules.Students.Enums;
using DEPI.Domain.Common.Base;
using System.ComponentModel.DataAnnotations;

namespace Depi.Domain.Modules.Students.Entities;

public class Student : BaseEntity
{
    public Guid UserId { get; set; }

    [MaxLength(200)]
    public string? University { get; set; }

    [MaxLength(150)]
    public string? Major { get; set; }

    [MaxLength(100)]
    public string? StudentId { get; set; }

    public decimal? GPA { get; set; }

    public GraduationStatus GraduationStatus { get; set; }

    public DateTime? ExpectedGraduationDate { get; set; }

    public StudentStatus Status { get; set; }

    // Navigation Properties
    public virtual ICollection<StudentPortfolioItem> PortfolioItems { get; set; } = new List<StudentPortfolioItem>();
    public virtual ICollection<TrainingProject> TrainingProjects { get; set; } = new List<TrainingProject>();
}