using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Application.Repositories.Projects;
using DEPI.Application.UseCases.Projects.CreateProject;
using DEPI.Domain.Entities.Projects;
using FluentValidation;
using MediatR;

namespace DEPI.Application.UseCases.Projects.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("عنوان المشروع مطلوب")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("وصف المشروع مطلوب")
            .MaximumLength(2000);

        RuleFor(x => x.BudgetMin)
            .GreaterThanOrEqualTo(0)
            .When(x => x.BudgetMin.HasValue);

        RuleFor(x => x.BudgetMax)
            .GreaterThanOrEqualTo(0)
            .When(x => x.BudgetMax.HasValue)
            .GreaterThanOrEqualTo(x => x.BudgetMin ?? 0);

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.UtcNow)
            .When(x => x.Deadline.HasValue);
    }
}

public class UpdateProjectCommandHandler
    : IRequestHandler<UpdateProjectCommand, Result<ProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Result<ProjectResponse>> Handle(
        UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
          
            var project = await _projectRepository.GetByIdAsync(request.ProjectId);

            if (project == null)
                return Result<ProjectResponse>.Failure(
                    Errors.NotFound("المشروع"),
                    ErrorCode.ProjectNotFound);


            if (project.OwnerId != request.OwnerId)
                return Result<ProjectResponse>.Failure(
                    "غير مصرح لك بتعديل هذا المشروع",
                    ErrorCode.Unauthorized);

            project.Update(
                request.Title,
                request.Description,
                request.BudgetMin,
                request.BudgetMax,
                request.Deadline,
                request.Skills,
                request.RequiredLevel,
                request.IsNda
            );

           
            await _projectRepository.UpdateAsync(project);

            
            return Result<ProjectResponse>.Success(project.ToResponse());
        }
        catch (Exception)
        {
            return Result<ProjectResponse>.Failure(
                Errors.Internal(),
                ErrorCode.InternalError);
        }
    }
}