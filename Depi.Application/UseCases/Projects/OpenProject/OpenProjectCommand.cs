// Projects/OpenProject/OpenProjectCommand.cs
using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using MediatR;
namespace DEPI.Application.UseCases.Projects.OpenProject;
public record OpenProjectCommand(Guid OwnerId, Guid ProjectId) : IRequest<Result<ProjectResponse>>;