using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.HeadHunters;
using DEPI.Domain.Entities.HeadHunters;
using MediatR;

namespace DEPI.Application.UseCases.HeadHunters;

public class HeadHunterResponse { public Guid Id { get; set; } public string UserId { get; set; } = string.Empty; public string Specialization { get; set; } = string.Empty; public string Bio { get; set; } = string.Empty; public HeadHunterStatus Status { get; set; } public int TotalAssignmentsCompleted { get; set; } public int SuccessfulPlacements { get; set; } public decimal SuccessRate { get; set; } public decimal CommissionRate { get; set; } }
public class AssignmentResponse { public Guid Id { get; set; } public Guid HeadHunterId { get; set; } public string Requirements { get; set; } = string.Empty; public AssignmentStatus Status { get; set; } public int CandidatesSubmitted { get; set; } public DateTime CreatedAt { get; set; } public DateTime ExpiresAt { get; set; } }
public class TalentRecommendationResponse { public Guid Id { get; set; } public Guid AssignmentId { get; set; } public string Reason { get; set; } = string.Empty; public string AIAnalysis { get; set; } = string.Empty; public decimal MatchScore { get; set; } public decimal ProfileScore { get; set; } public RecommendationResult Result { get; set; } public DateTime CreatedAt { get; set; } }

public record CreateHeadHunterRequest(string Specialization, string Bio);
public record CreateAssignmentRequest(string Requirements, string? ProjectId, string? JobId, DateTime? ExpiresAt);
public record SubmitTalentRequest(Guid AssignmentId, string RecommendedUserId, string Reason, string AIAnalysis, decimal MatchScore, decimal ProfileScore, decimal SkillsScore, decimal ExperienceScore);
public record ReviewTalentRequest(Guid RecommendationId, bool IsAccepted, string? Feedback);

public record GetHeadHuntersQuery(string? Specialization) : IRequest<List<HeadHunterResponse>>;
public record GetTopHeadHuntersQuery(int Count) : IRequest<List<HeadHunterResponse>>;
public record CreateHeadHunterCommand(Guid UserId, CreateHeadHunterRequest Request) : IRequest<HeadHunterResponse>;
public record CreateAssignmentCommand(Guid ClientId, Guid HunterId, CreateAssignmentRequest Request) : IRequest<AssignmentResponse>;
public record GetMyAssignmentsQuery(Guid HunterId) : IRequest<List<AssignmentResponse>>;
public record GetClientAssignmentsQuery(Guid ClientId) : IRequest<List<AssignmentResponse>>;
public record SubmitTalentCommand(Guid HunterId, SubmitTalentRequest Request) : IRequest<TalentRecommendationResponse>;
public record ReviewTalentCommand(Guid ClientId, ReviewTalentRequest Request) : IRequest<TalentRecommendationResponse>;
public record GetRecommendationsQuery(Guid AssignmentId) : IRequest<List<TalentRecommendationResponse>>;

public class GetHeadHuntersQueryHandler : IRequestHandler<GetHeadHuntersQuery, List<HeadHunterResponse>>
{
    private readonly IHeadHunterRepository _repo; private readonly IMapper _mapper;
    public GetHeadHuntersQueryHandler(IHeadHunterRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<HeadHunterResponse>> Handle(GetHeadHuntersQuery r, CancellationToken ct) => _mapper.Map<List<HeadHunterResponse>>(string.IsNullOrWhiteSpace(r.Specialization) ? await _repo.GetActiveAsync() : await _repo.GetBySpecializationAsync(r.Specialization));
}

public class GetTopHeadHuntersQueryHandler : IRequestHandler<GetTopHeadHuntersQuery, List<HeadHunterResponse>>
{
    private readonly IHeadHunterRepository _repo; private readonly IMapper _mapper;
    public GetTopHeadHuntersQueryHandler(IHeadHunterRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<HeadHunterResponse>> Handle(GetTopHeadHuntersQuery r, CancellationToken ct) => _mapper.Map<List<HeadHunterResponse>>(await _repo.GetTopPerformersAsync(r.Count));
}

public class CreateHeadHunterCommandHandler : IRequestHandler<CreateHeadHunterCommand, HeadHunterResponse>
{
    private readonly IHeadHunterRepository _repo; private readonly IMapper _mapper;
    public CreateHeadHunterCommandHandler(IHeadHunterRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<HeadHunterResponse> Handle(CreateHeadHunterCommand r, CancellationToken ct)
    {
        var existing = await _repo.GetByUserIdAsync(r.UserId.ToString());
        if (existing != null) throw new InvalidOperationException(Errors.AlreadyExists("HeadHunter"));
        var hunter = new HeadHunter { UserId = r.UserId.ToString(), Specialization = r.Request.Specialization, Bio = r.Request.Bio, Status = HeadHunterStatus.Active, CommissionRate = 0.05m, LastActiveAt = DateTime.UtcNow };
        await _repo.AddAsync(hunter, ct);
        return _mapper.Map<HeadHunterResponse>(hunter);
    }
}

public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, AssignmentResponse>
{
    private readonly IHeadHunterAssignmentRepository _repo; private readonly IHeadHunterRepository _hunterRepo; private readonly IMapper _mapper;
    public CreateAssignmentCommandHandler(IHeadHunterAssignmentRepository repo, IHeadHunterRepository hunterRepo, IMapper mapper) { _repo = repo; _hunterRepo = hunterRepo; _mapper = mapper; }
    public async Task<AssignmentResponse> Handle(CreateAssignmentCommand r, CancellationToken ct)
    {
        var hunter = await _hunterRepo.GetByIdAsync(r.HunterId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("HeadHunter"));
        var assignment = new HeadHunterAssignment { HeadHunterId = r.HunterId, ClientId = r.ClientId, Requirements = r.Request.Requirements, ProjectId = r.Request.ProjectId, JobId = r.Request.JobId, Status = AssignmentStatus.Active, ExpiresAt = r.Request.ExpiresAt ?? DateTime.UtcNow.AddDays(30) };
        await _repo.AddAsync(assignment, ct);
        hunter.IncrementAssignments(); await _hunterRepo.UpdateAsync(hunter, ct);
        return _mapper.Map<AssignmentResponse>(assignment);
    }
}

public class GetMyAssignmentsQueryHandler : IRequestHandler<GetMyAssignmentsQuery, List<AssignmentResponse>>
{
    private readonly IHeadHunterAssignmentRepository _repo; private readonly IMapper _mapper;
    public GetMyAssignmentsQueryHandler(IHeadHunterAssignmentRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<AssignmentResponse>> Handle(GetMyAssignmentsQuery r, CancellationToken ct) => _mapper.Map<List<AssignmentResponse>>(await _repo.GetByHunterIdAsync(r.HunterId));
}

public class GetClientAssignmentsQueryHandler : IRequestHandler<GetClientAssignmentsQuery, List<AssignmentResponse>>
{
    private readonly IHeadHunterAssignmentRepository _repo; private readonly IMapper _mapper;
    public GetClientAssignmentsQueryHandler(IHeadHunterAssignmentRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<AssignmentResponse>> Handle(GetClientAssignmentsQuery r, CancellationToken ct) => _mapper.Map<List<AssignmentResponse>>(await _repo.GetByClientIdAsync(r.ClientId.ToString()));
}

public class SubmitTalentCommandHandler : IRequestHandler<SubmitTalentCommand, TalentRecommendationResponse>
{
    private readonly ITalentRecommendationRepository _repo; private readonly IHeadHunterAssignmentRepository _aRepo; private readonly IHeadHunterRepository _hRepo; private readonly IMapper _mapper;
    public SubmitTalentCommandHandler(ITalentRecommendationRepository repo, IHeadHunterAssignmentRepository aRepo, IHeadHunterRepository hRepo, IMapper mapper) { _repo = repo; _aRepo = aRepo; _hRepo = hRepo; _mapper = mapper; }
    public async Task<TalentRecommendationResponse> Handle(SubmitTalentCommand r, CancellationToken ct)
    {
        var assignment = await _aRepo.GetByIdAsync(r.Request.AssignmentId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Assignment"));
        var rec = new TalentRecommendation { AssignmentId = r.Request.AssignmentId, RecommendedUserId = r.Request.RecommendedUserId, Reason = r.Request.Reason, AIAnalysis = r.Request.AIAnalysis, MatchScore = r.Request.MatchScore, ProfileScore = r.Request.ProfileScore, SkillsScore = r.Request.SkillsScore, ExperienceScore = r.Request.ExperienceScore };
        await _repo.AddAsync(rec, ct);
        assignment.IncrementCandidates(); await _aRepo.UpdateAsync(assignment, ct);
        var hunter = await _hRepo.GetByIdAsync(assignment.HeadHunterId, ct);
        if (hunter != null) { hunter.IncrementSubmissions(); await _hRepo.UpdateAsync(hunter, ct); }
        return _mapper.Map<TalentRecommendationResponse>(rec);
    }
}

public class ReviewTalentCommandHandler : IRequestHandler<ReviewTalentCommand, TalentRecommendationResponse>
{
    private readonly ITalentRecommendationRepository _repo; private readonly IMapper _mapper;
    public ReviewTalentCommandHandler(ITalentRecommendationRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<TalentRecommendationResponse> Handle(ReviewTalentCommand r, CancellationToken ct)
    {
        var rec = await _repo.GetByIdAsync(r.Request.RecommendationId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Recommendation"));
        if (r.Request.IsAccepted) rec.Accept(); else rec.Reject(r.Request.Feedback);
        await _repo.UpdateAsync(rec, ct);
        return _mapper.Map<TalentRecommendationResponse>(rec);
    }
}

public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, List<TalentRecommendationResponse>>
{
    private readonly ITalentRecommendationRepository _repo; private readonly IMapper _mapper;
    public GetRecommendationsQueryHandler(ITalentRecommendationRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<TalentRecommendationResponse>> Handle(GetRecommendationsQuery r, CancellationToken ct) => _mapper.Map<List<TalentRecommendationResponse>>(await _repo.GetByAssignmentIdAsync(r.AssignmentId));
}
