using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Application.Repositories.Projects;
using DEPI.Application.UseCases.Projects.CreateProject;
using DEPI.Domain.Enums;
using MediatR;

namespace DEPI.Application.UseCases.Projects.Queries;

public record GetProjectQuery(Guid Id) : IRequest<Result<ProjectResponse>>;

public class GetProjectQueryHandler
    : IRequestHandler<GetProjectQuery, Result<ProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectResponse>> Handle(
        GetProjectQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project == null)
            return Result<ProjectResponse>.Failure(
                Errors.NotFound("المشروع"),
                ErrorCode.ProjectNotFound);

        return Result<ProjectResponse>.Success(project.ToResponse()); // محدش يركز اوي هنا دا Pattern ثابت = مثلا ("Get Query Success")
    }
}

public record GetProjectsQuery(
    ProjectStatus? Status,
    ProjectType? Type,
    ExperienceLevel? RequiredLevel,
    Guid? CategoryId,
    decimal? MinBudget,
    decimal? MaxBudget,
    string? Search,
    Guid? OwnerId,
    int Page = 1,
    int PageSize = 20
) : IRequest<Result<ProjectListResponse>>;

public class GetProjectsQueryHandler
    : IRequestHandler<GetProjectsQuery, Result<ProjectListResponse>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectListResponse>> Handle(
        GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetAllProjectsAsync(cancellationToken);
        var filteredProjects = projects.AsEnumerable();

        if (request.Status.HasValue)
            filteredProjects = filteredProjects.Where(p => p.Status == request.Status);
        if (request.Type.HasValue)
            filteredProjects = filteredProjects.Where(p => p.Type == request.Type);
        if (request.RequiredLevel.HasValue)
            filteredProjects = filteredProjects.Where(p => p.RequiredLevel == request.RequiredLevel);
        if (request.CategoryId.HasValue)
            filteredProjects = filteredProjects.Where(p => p.CategoryId == request.CategoryId);
        if (request.MinBudget.HasValue)
            filteredProjects = filteredProjects.Where(p => p.BudgetMin >= request.MinBudget);
        if (request.MaxBudget.HasValue)
            filteredProjects = filteredProjects.Where(p => p.BudgetMax <= request.MaxBudget);
        if (!string.IsNullOrEmpty(request.Search))
            filteredProjects = filteredProjects.Where(p => p.Title.Contains(request.Search) || p.Description.Contains(request.Search));
        if (request.OwnerId.HasValue)
            filteredProjects = filteredProjects.Where(p => p.OwnerId == request.OwnerId);

        var totalCount = filteredProjects.Count();
        var pagedProjects = filteredProjects
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        return Result<ProjectListResponse>.Success(new ProjectListResponse(
            Projects: pagedProjects.Select(p => p.ToResponse()).ToList(),
            TotalCount: totalCount,
            Page: request.Page,
            PageSize: request.PageSize,
            TotalPages: totalPages));
    }
}