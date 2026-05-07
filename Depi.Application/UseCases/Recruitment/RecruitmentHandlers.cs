using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Recruitment;
using DEPI.Domain.Entities.Recruitment;
using MediatR;

namespace DEPI.Application.UseCases.Recruitment;

public class JobResponse { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public JobType Type { get; set; } public JobStatus Status { get; set; } public decimal BudgetMin { get; set; } public decimal BudgetMax { get; set; } public string BudgetType { get; set; } = string.Empty; public string ExperienceLevel { get; set; } = string.Empty; public string Location { get; set; } = string.Empty; public bool IsRemote { get; set; } public bool IsFeatured { get; set; } public int ViewsCount { get; set; } public int ApplicationsCount { get; set; } public string SkillsRequired { get; set; } = string.Empty; public DateTime ExpiresAt { get; set; } public DateTime CreatedAt { get; set; } }
public class JobApplicationResponse { public Guid Id { get; set; } public Guid JobId { get; set; } public string CoverLetter { get; set; } = string.Empty; public string ProposedRate { get; set; } = string.Empty; public ApplicationStatus Status { get; set; } public int AIMatchScore { get; set; } public DateTime CreatedAt { get; set; } }

public record CreateJobRequest(string Title, string Description, JobType Type, decimal BudgetMin, decimal BudgetMax, string BudgetType, string ExperienceLevel, string Location, bool IsRemote, string SkillsRequired, DateTime ExpiresAt, Guid? CompanyId);
public record ApplyJobRequest(Guid JobId, string CoverLetter, string ProposedRate, string ProposedTimeline);

public record CreateJobCommand(Guid OwnerId, CreateJobRequest Request) : IRequest<JobResponse>;
public record GetJobsQuery(string? SearchTerm, JobType? Type, string? Location, bool? Featured, int Page, int PageSize) : IRequest<List<JobResponse>>;
public record ApplyJobCommand(Guid ApplicantId, ApplyJobRequest Request) : IRequest<JobApplicationResponse>;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobResponse>
{
    private readonly IJobRepository _repo; private readonly IMapper _mapper;
    public CreateJobCommandHandler(IJobRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<JobResponse> Handle(CreateJobCommand r, CancellationToken ct)
    {
        var job = new Job { OwnerId = r.OwnerId, CompanyId = r.Request.CompanyId ?? Guid.Empty, Title = r.Request.Title, Description = r.Request.Description, Type = r.Request.Type, Status = JobStatus.Active, BudgetMin = r.Request.BudgetMin, BudgetMax = r.Request.BudgetMax, BudgetType = r.Request.BudgetType, ExperienceLevel = r.Request.ExperienceLevel, Location = r.Request.Location, IsRemote = r.Request.IsRemote, SkillsRequired = r.Request.SkillsRequired, ExpiresAt = r.Request.ExpiresAt };
        await _repo.AddAsync(job, ct);
        return _mapper.Map<JobResponse>(job);
    }
}

public class GetJobsQueryHandler : IRequestHandler<GetJobsQuery, List<JobResponse>>
{
    private readonly IJobRepository _repo; private readonly IMapper _mapper;
    public GetJobsQueryHandler(IJobRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<JobResponse>> Handle(GetJobsQuery r, CancellationToken ct)
    {
        List<Job> jobs;
        if (r.Featured == true) jobs = await _repo.GetFeaturedJobsAsync(r.PageSize);
        else if (!string.IsNullOrWhiteSpace(r.SearchTerm) || r.Type.HasValue || !string.IsNullOrWhiteSpace(r.Location)) jobs = await _repo.SearchJobsAsync(r.SearchTerm ?? "", r.Type, r.Location);
        else jobs = await _repo.GetActiveJobsAsync();
        return _mapper.Map<List<JobResponse>>(jobs);
    }
}

public class ApplyJobCommandHandler : IRequestHandler<ApplyJobCommand, JobApplicationResponse>
{
    private readonly IJobApplicationRepository _repo; private readonly IJobRepository _jobRepo; private readonly IMapper _mapper;
    public ApplyJobCommandHandler(IJobApplicationRepository repo, IJobRepository jobRepo, IMapper mapper) { _repo = repo; _jobRepo = jobRepo; _mapper = mapper; }
    public async Task<JobApplicationResponse> Handle(ApplyJobCommand r, CancellationToken ct)
    {
        var job = await _jobRepo.GetByIdAsync(r.Request.JobId, ct) ?? throw new KeyNotFoundException(Errors.NotFound("Job"));
        var application = new JobApplication { JobId = r.Request.JobId, ApplicantId = r.ApplicantId, CoverLetter = r.Request.CoverLetter, ProposedRate = r.Request.ProposedRate, ProposedTimeline = r.Request.ProposedTimeline };
        await _repo.AddAsync(application, ct);
        return _mapper.Map<JobApplicationResponse>(application);
    }
}
