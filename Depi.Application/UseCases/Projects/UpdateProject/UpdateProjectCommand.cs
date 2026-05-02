// Projects/UpdateProject/UpdateProjectCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Domain.Entities.Projects;
using MediatR;
namespace DEPI.Application.UseCases.Projects.UpdateProject;
public record UpdateProjectCommand(Guid OwnerId, Guid ProjectId, string Title, string Description, decimal? BudgetMin, decimal? BudgetMax, DateTime? Deadline, string? Skills, ExperienceLevel RequiredLevel, bool IsNda) : IRequest<Result<ProjectResponse>>;