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

public record ProjectResponse(
    Guid Id,
    Guid OwnerId,
    string OwnerName,
    Guid? CategoryId,
    string? CategoryName,
    string Title,
    string Description,
    ProjectType Type,
    ProjectStatus Status,
    decimal? BudgetMin,
    decimal? BudgetMax,
    decimal? FixedPrice,
    int? EstimatedHours,
    ExperienceLevel RequiredLevel,
    DateTime? Deadline,
    string? Skills,
    bool IsFeatured,
    bool IsUrgent,
    bool IsNda,
    int ViewsCount,
    int ProposalsCount,
    Guid? AssignedFreelancerId,
    string? AssignedFreelancerName,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    decimal? FinalPrice,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record ProjectListResponse(
    List<ProjectResponse> Projects,
    int TotalCount,
    int Page,
    int PageSize,
    int TotalPages
);