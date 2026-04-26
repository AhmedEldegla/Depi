using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Application.UseCases.Projects.CreateProject;
using MediatR;

using DEPI.Application.Repositories.Projects;
namespace DEPI.Application.UseCases.Projects.OpenProject;
public class OpenProjectCommandHandler : IRequestHandler<OpenProjectCommand, Result<ProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;
    public OpenProjectCommandHandler(IProjectRepository projectRepository) { _projectRepository = projectRepository; }
    public async Task<Result<ProjectResponse>> Handle(OpenProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (project == null)
            return Result<ProjectResponse>.Failure(Errors.NotFound("المشروع"), ErrorCode.ProjectNotFound);


        if (project.OwnerId != request.OwnerId) 
            return Result<ProjectResponse>.Failure(Errors.Forbidden(), ErrorCode.Forbidden);

        try 
        { 
            project.Open();
            await _projectRepository.UpdateAsync(project);
            return Result<ProjectResponse>.Success(project.ToResponse());
        }
        catch (InvalidOperationException ex)
        { 
            return Result<ProjectResponse>.Failure(ex.Message, ErrorCode.ValidationError);
        }
    }
}