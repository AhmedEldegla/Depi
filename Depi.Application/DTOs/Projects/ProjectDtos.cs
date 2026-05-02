using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Enums;

namespace DEPI.Application.DTOs.Projects;

public record CreateProjectRequest(
    string Title,
    string Description,
    ProjectType Type,
    decimal? BudgetMin,
    decimal? BudgetMax,
    decimal? FixedPrice,
    int? EstimatedDays,
    ExperienceLevel RequiredLevel,
    DateTime? Deadline,
    string? Skills,
    bool IsNda,
    Guid? CategoryId
);

public record UpdateProjectRequest(
    string Title,
    string Description,
    decimal? BudgetMin,
    decimal? BudgetMax,
    int? EstimatedDays,
    ExperienceLevel RequiredLevel,
    DateTime? Deadline,
    string? Skills,
    bool IsNda
);

public record ProjectFilterRequest(
    ProjectStatus? Status,
    ProjectType? Type,
    ExperienceLevel? RequiredLevel,
    Guid? CategoryId,
    decimal? MinBudget,
    decimal? MaxBudget,
    string? Search,
    int Page = 1,
    int PageSize = 20
);

public class ProjectResponse
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectType Type { get; set; }
    public ProjectStatus Status { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public decimal? FixedPrice { get; set; }
    public int? EstimatedHours { get; set; }
    public ExperienceLevel RequiredLevel { get; set; }
    public DateTime? Deadline { get; set; }
    public string? Skills { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsUrgent { get; set; }
    public bool IsNda { get; set; }
    public int ViewsCount { get; set; }
    public int ProposalsCount { get; set; }
    public Guid? AssignedFreelancerId { get; set; }
    public string? AssignedFreelancerName { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal? FinalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ProjectListResponse
{
    public List<ProjectResponse> Projects { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}