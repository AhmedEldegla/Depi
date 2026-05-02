using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Projects.OpenProject;

public class OpenProjectCommandHandler : IRequestHandler<OpenProjectCommand, Result<ProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public OpenProjectCommandHandler(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProjectResponse>> Handle(OpenProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (project == null) return Result<ProjectResponse>.Failure(Errors.NotFound("المشروع"), ErrorCode.ProjectNotFound);
        if (project.OwnerId != request.OwnerId) return Result<ProjectResponse>.Failure(Errors.Forbidden(), ErrorCode.Forbidden);

        try
        {
            project.Open();
            await _projectRepository.UpdateAsync(project);
            return Result<ProjectResponse>.Success(_mapper.Map<ProjectResponse>(project));
        }
        catch (InvalidOperationException ex)
        {
            return Result<ProjectResponse>.Failure(ex.Message, ErrorCode.ValidationError);
        }
    }
}