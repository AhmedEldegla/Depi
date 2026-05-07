using DEPI.Domain.Entities.Recruitment;

namespace DEPI.Application.DTOs.Recruitment;

public class JobResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public JobType Type { get; set; }
    public JobStatus Status { get; set; }
    public decimal BudgetMin { get; set; }
    public decimal BudgetMax { get; set; }
    public string BudgetType { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool IsRemote { get; set; }
    public bool IsFeatured { get; set; }
    public int ViewsCount { get; set; }
    public int ApplicationsCount { get; set; }
    public string SkillsRequired { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class JobApplicationResponse
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public string CoverLetter { get; set; } = string.Empty;
    public string ProposedRate { get; set; } = string.Empty;
    public ApplicationStatus Status { get; set; }
    public int AIMatchScore { get; set; }
    public DateTime CreatedAt { get; set; }
}