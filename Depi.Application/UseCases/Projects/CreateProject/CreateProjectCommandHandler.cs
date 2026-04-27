// Projects/CreateProject/CreateProjectCommandHandler.cs (includes Validator +Handler +Extensions)
using Depi.Application.Repositories.Identity;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Application.Repositories.Projects;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Enums;
using FluentValidation;
using MediatR;
namespace DEPI.Application.UseCases.Projects.CreateProject;
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("عنوان المشروع مطلوب").MaximumLength(200).WithMessage("العنوان يجب ألا يتجاوز 200 حرف");
        RuleFor(x => x.Description).NotEmpty().WithMessage("وصف المشروع مطلوب").MaximumLength(2000).WithMessage("الوصف يجب ألا يتجاوز 2000 حرف");
        RuleFor(x => x.Type).IsInEnum().WithMessage("نوع المشروع غير صالح");
        RuleFor(x => x.RequiredLevel).IsInEnum().WithMessage("مستوى الخبرة غير صالح");
        RuleFor(x => x.BudgetMin).GreaterThanOrEqualTo(0).WithMessage("الميزانية الدنيا يجب أن تكون أكبر من صفر");
        RuleFor(x => x.BudgetMax).GreaterThanOrEqualTo(0).WithMessage("الميزانية القصوى يجب أن تكون أكبر من صفر").GreaterThanOrEqualTo(x => x.BudgetMin ?? 0).WithMessage("الميزانية القصوى يجب أن تكون أكبر من الدنيا");
        RuleFor(x => x.FixedPrice).GreaterThanOrEqualTo(0).WithMessage("السعر الثابت يجب أن يكون أكبر من صفر");
        RuleFor(x => x.Deadline).GreaterThan(DateTime.UtcNow).WithMessage("الموعد النهائي يجب أن يكون في المستقبل");
    }
}
public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result<ProjectResponse>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUserRepository userRepository)
    { 
        _projectRepository = projectRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ProjectResponse>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var owner = await _userRepository.GetByIdAsync(request.OwnerId) ?? throw new KeyNotFoundException(Errors.NotFound("User"));
            owner.EnsureVerifiedFor("نشر المشروع");
            
            var project = Project.Create(request.OwnerId, request.Title, request.Description, request.Type, request.BudgetMin, request.BudgetMax, request.FixedPrice, request.RequiredLevel, request.Deadline);
            project.SetCreatedBy(request.OwnerId);
            
            
            if (!string.IsNullOrWhiteSpace(request.Skills))
                project.SetSkills(request.Skills);


            await _projectRepository.AddAsync(project);
            return Result<ProjectResponse>.Success(project.ToResponse());
        }
        catch (ArgumentException ex)
        { 
            return Result<ProjectResponse>.Failure(ex.Message, ErrorCode.ValidationError);
        }
        catch (Exception ex)
        {
            return Result<ProjectResponse>.Failure(ex.Message, ErrorCode.InternalError);
        }
    }
}
public static class ProjectExtensions {
    public static ProjectResponse ToResponse(this Project project) => new ProjectResponse(Id: project.Id, OwnerId: project.OwnerId, OwnerName: project.Owner?.FullName ?? "Unknown", CategoryId: project.CategoryId, CategoryName: project.Category?.Name, Title: project.Title, Description: project.Description, Type: project.Type, Status: project.Status, BudgetMin: project.BudgetMin, BudgetMax: project.BudgetMax, FixedPrice: project.FixedPrice, EstimatedHours: project.RequiredLevel == ExperienceLevel.Beginner ? 10 : project.RequiredLevel == ExperienceLevel.Intermediate ? 40 : 80, RequiredLevel: project.RequiredLevel, Deadline: project.Deadline, Skills: project.Skills, IsFeatured: project.IsFeatured, IsUrgent: project.IsUrgent, IsNda: project.IsNda, ViewsCount: project.ViewsCount, ProposalsCount: project.ProposalsCount, AssignedFreelancerId: project.AssignedFreelancerId, AssignedFreelancerName: project.AssignedFreelancer?.FullName, StartedAt: project.StartedAt, CompletedAt: project.CompletedAt, FinalPrice: project.FinalPrice, CreatedAt: project.CreatedAt, UpdatedAt: project.UpdatedAt);
}