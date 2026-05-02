using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Projects;
using DEPI.Application.Interfaces;
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
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _mapper = mapper;
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
            return Result<ProjectResponse>.Success(_mapper.Map<ProjectResponse>(project));
        }
        catch (ArgumentException ex) { return Result<ProjectResponse>.Failure(ex.Message, ErrorCode.ValidationError); }
        catch (Exception) { return Result<ProjectResponse>.Failure(Errors.Internal(), ErrorCode.InternalError); }
    }
}