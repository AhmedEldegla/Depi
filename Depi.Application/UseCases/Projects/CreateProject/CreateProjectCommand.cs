// Projects/CreateProject/CreateProjectCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Enums;
using MediatR;
namespace DEPI.Application.UseCases.Projects.CreateProject;
public record CreateProjectCommand(Guid OwnerId, string Title, string Description, ProjectType Type, decimal? BudgetMin, decimal? BudgetMax, decimal? FixedPrice, ExperienceLevel RequiredLevel, DateTime? Deadline, string? Skills, bool IsNda, Guid? CategoryId) : IRequest<Result<ProjectResponse>>;