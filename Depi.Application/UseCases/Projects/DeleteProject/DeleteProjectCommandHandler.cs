using DEPI.Application.Common;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Projects.DeleteProject;

public record DeleteProjectCommand(Guid ProjectId, Guid UserId) : IRequest;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
            ?? throw new KeyNotFoundException(Errors.NotFound("Project"));

        if (project.OwnerId != request.UserId)
            throw new UnauthorizedAccessException(Errors.Forbidden());

        await _projectRepository.DeleteAsync(project.Id, cancellationToken);
    }
}
