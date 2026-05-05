using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Coaching;
using DEPI.Application.Repositories.Students;
using DEPI.Domain.Entities.Students;
using MediatR;

namespace DEPI.Application.UseCases.Students;

public class StudentProfileResponse { public Guid Id { get; set; } public string PhoneNumber { get; set; } = string.Empty; public string Bio { get; set; } = string.Empty; public DateTime EnrollmentDate { get; set; } public StudentStatus Status { get; set; } public OnboardingStep CurrentStep { get; set; } public int CompletedSteps { get; set; } public int TotalSteps { get; set; } public decimal ProgressPercentage { get; set; } public bool HasCompletedPortfolio { get; set; } public decimal SkillsAssessmentScore { get; set; } public bool HasCompletedSkillsAssessment { get; set; } public decimal ReadinessScore { get; set; } public bool IsReadyForMarket { get; set; } public string Skills { get; set; } = string.Empty; public string Goals { get; set; } = string.Empty; public int CompletedCourses { get; set; } public int CompletedProjects { get; set; } public decimal AverageRating { get; set; } public StudentLevel Level { get; set; } public string? AssignedCoachId { get; set; } public DateTime? FirstProjectAt { get; set; } public DateTime? PromotedToFreelancerAt { get; set; } }
public class OnboardingSummaryResponse { public StudentProfileResponse Profile { get; set; } = new(); public int PortfolioItemsCount { get; set; } public int CompletedCourses { get; set; } public int UpcomingSessions { get; set; } public string NextAction { get; set; } = string.Empty; public string Tip { get; set; } = string.Empty; }

public record CreateStudentProfileRequest(string PhoneNumber, string? Bio, string? Skills, string? Goals);
public record CompletePortfolioStepRequest(int PortfolioItemsCount);
public record CompleteSkillsStepRequest(decimal AssessmentScore);
public record AssignCoachRequest(Guid CoachId);

public record GetMyStudentProfileQuery(Guid UserId) : IRequest<StudentProfileResponse?>;
public record GetStudentOnboardingSummaryQuery(Guid UserId) : IRequest<OnboardingSummaryResponse?>;
public record CreateStudentProfileCommand(Guid UserId, CreateStudentProfileRequest Request) : IRequest<StudentProfileResponse>;
public record CompleteStudentPortfolioCommand(Guid UserId, CompletePortfolioStepRequest Request) : IRequest<StudentProfileResponse>;
public record CompleteStudentSkillsCommand(Guid UserId, CompleteSkillsStepRequest Request) : IRequest<StudentProfileResponse>;
public record AssignStudentCoachCommand(Guid StudentId, Guid CoachId) : IRequest<StudentProfileResponse>;
public record PromoteStudentToFreelancerCommand(Guid UserId, string PromotedBy) : IRequest<StudentProfileResponse>;
public record GetReadyStudentsQuery : IRequest<List<StudentProfileResponse>>;

public class GetMyStudentProfileQueryHandler : IRequestHandler<GetMyStudentProfileQuery, StudentProfileResponse?>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public GetMyStudentProfileQueryHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<StudentProfileResponse?> Handle(GetMyStudentProfileQuery r, CancellationToken ct) => (await _repo.GetByUserIdAsync(r.UserId)) is { } s ? _mapper.Map<StudentProfileResponse>(s) : null;
}

public class GetStudentOnboardingSummaryQueryHandler : IRequestHandler<GetStudentOnboardingSummaryQuery, OnboardingSummaryResponse?>
{
    private readonly IStudentProfileRepository _studentRepo; private readonly ICoachingSessionRepository _coachingRepo; private readonly IMapper _mapper;
    public GetStudentOnboardingSummaryQueryHandler(IStudentProfileRepository sRepo, ICoachingSessionRepository cRepo, IMapper mapper) { _studentRepo = sRepo; _coachingRepo = cRepo; _mapper = mapper; }
    public async Task<OnboardingSummaryResponse?> Handle(GetStudentOnboardingSummaryQuery r, CancellationToken ct)
    {
        var profile = await _studentRepo.GetByUserIdAsync(r.UserId);
        if (profile == null) return null;
        var upcomingSessions = await _coachingRepo.GetUpcomingAsync(r.UserId);
        return new OnboardingSummaryResponse
        {
            Profile = _mapper.Map<StudentProfileResponse>(profile),
            PortfolioItemsCount = profile.PortfolioItemsCount, CompletedCourses = profile.CompletedCourses,
            UpcomingSessions = upcomingSessions.Count,
            NextAction = profile.CurrentStep switch { OnboardingStep.Profile => "Complete your profile", OnboardingStep.Portfolio => "Build your portfolio", OnboardingStep.Skills => "Complete skills assessment", OnboardingStep.Coach => "Get assigned to a coach", OnboardingStep.FirstProject => "Apply for your first project", OnboardingStep.Graduation => "Finalize graduation", _ => "You're all set!" },
            Tip = profile.CurrentStep switch { OnboardingStep.Portfolio => "Add real projects you built", OnboardingStep.Skills => "Be honest about skill level", OnboardingStep.Coach => "A coach will guide your first project", OnboardingStep.FirstProject => "Start with small projects", OnboardingStep.Graduation => "Almost ready for promotion!", _ => "Keep progressing!" }
        };
    }
}

public class CreateStudentProfileCommandHandler : IRequestHandler<CreateStudentProfileCommand, StudentProfileResponse>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public CreateStudentProfileCommandHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<StudentProfileResponse> Handle(CreateStudentProfileCommand r, CancellationToken ct)
    {
        var existing = await _repo.GetByUserIdAsync(r.UserId);
        if (existing != null) throw new InvalidOperationException(Errors.AlreadyExists("StudentProfile"));
        var profile = new StudentProfile { UserId = r.UserId, PhoneNumber = r.Request.PhoneNumber, Bio = r.Request.Bio ?? "", Skills = r.Request.Skills ?? "", Goals = r.Request.Goals ?? "", EnrollmentDate = DateTime.UtcNow };
        await _repo.AddAsync(profile, ct);
        return _mapper.Map<StudentProfileResponse>(profile);
    }
}

public class CompleteStudentPortfolioCommandHandler : IRequestHandler<CompleteStudentPortfolioCommand, StudentProfileResponse>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public CompleteStudentPortfolioCommandHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<StudentProfileResponse> Handle(CompleteStudentPortfolioCommand r, CancellationToken ct)
    {
        var profile = await _repo.GetByUserIdAsync(r.UserId) ?? throw new KeyNotFoundException(Errors.NotFound("StudentProfile"));
        if (r.Request.PortfolioItemsCount >= 3) { profile.CompletePortfolio(); profile.CompleteStep(); }
        await _repo.UpdateAsync(profile, ct);
        return _mapper.Map<StudentProfileResponse>(profile);
    }
}

public class CompleteStudentSkillsCommandHandler : IRequestHandler<CompleteStudentSkillsCommand, StudentProfileResponse>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public CompleteStudentSkillsCommandHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<StudentProfileResponse> Handle(CompleteStudentSkillsCommand r, CancellationToken ct)
    {
        var profile = await _repo.GetByUserIdAsync(r.UserId) ?? throw new KeyNotFoundException(Errors.NotFound("StudentProfile"));
        profile.CompleteSkillsAssessment(r.Request.AssessmentScore); profile.CompleteStep();
        if (profile.ReadinessScore >= 60) profile.IsReadyForMarket = true;
        await _repo.UpdateAsync(profile, ct);
        return _mapper.Map<StudentProfileResponse>(profile);
    }
}

public class AssignStudentCoachCommandHandler : IRequestHandler<AssignStudentCoachCommand, StudentProfileResponse>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public AssignStudentCoachCommandHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<StudentProfileResponse> Handle(AssignStudentCoachCommand r, CancellationToken ct)
    {
        var profile = await _repo.GetByUserIdAsync(r.StudentId) ?? throw new KeyNotFoundException(Errors.NotFound("StudentProfile"));
        profile.AssignCoach(r.CoachId); profile.CompleteStep();
        await _repo.UpdateAsync(profile, ct);
        return _mapper.Map<StudentProfileResponse>(profile);
    }
}

public class PromoteStudentToFreelancerCommandHandler : IRequestHandler<PromoteStudentToFreelancerCommand, StudentProfileResponse>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public PromoteStudentToFreelancerCommandHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<StudentProfileResponse> Handle(PromoteStudentToFreelancerCommand r, CancellationToken ct)
    {
        var profile = await _repo.GetByUserIdAsync(r.UserId) ?? throw new KeyNotFoundException(Errors.NotFound("StudentProfile"));
        profile.PromoteToFreelancer(r.PromotedBy); profile.Graduate();
        await _repo.UpdateAsync(profile, ct);
        return _mapper.Map<StudentProfileResponse>(profile);
    }
}

public class GetReadyStudentsQueryHandler : IRequestHandler<GetReadyStudentsQuery, List<StudentProfileResponse>>
{
    private readonly IStudentProfileRepository _repo; private readonly IMapper _mapper;
    public GetReadyStudentsQueryHandler(IStudentProfileRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<StudentProfileResponse>> Handle(GetReadyStudentsQuery r, CancellationToken ct) => _mapper.Map<List<StudentProfileResponse>>(await _repo.GetReadyForMarketAsync());
}
