using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Learning;
using DEPI.Domain.Entities.Learning;
using MediatR;

namespace DEPI.Application.UseCases.Learning;

public record CreateCourseLessonRequest(Guid CourseId, string Title, string Description, string? VideoUrl, int Duration, int DisplayOrder, bool IsFree, LessonType Type);
public record UpdateLessonProgressRequest(Guid LessonId, bool IsCompleted, int WatchTime, string? LastPosition);
public record CreateLearningPathRequest(string Title, string Description, string? ThumbnailUrl, bool IsFeatured, decimal Price, bool IsFree, string? Category);
public record CreateCertificationRequest(string Title, string Description, string IssuingOrganization, string? CredentialUrl, string? CredentialId, DateTime IssueDate, DateTime? ExpiryDate, string? BadgeUrl, string? Category);
public record CreateCourseReviewRequest(Guid CourseId, string Title, string Content, int Rating);

public record GetCourseLessonsQuery(Guid CourseId) : IRequest<List<CourseLessonResponse>>;
public record CreateCourseLessonCommand(Guid InstructorId, CreateCourseLessonRequest Request) : IRequest<CourseLessonResponse>;
public record GetLessonProgressQuery(Guid UserId, Guid CourseId) : IRequest<List<LessonProgressResponse>>;
public record UpdateLessonProgressCommand(Guid UserId, UpdateLessonProgressRequest Request) : IRequest<Unit>;
public record GetLearningPathsQuery(bool? Published, bool? Featured, int Page, int PageSize) : IRequest<List<LearningPathResponse>>;
public record CreateLearningPathCommand(Guid InstructorId, CreateLearningPathRequest Request) : IRequest<LearningPathResponse>;
public record GetCertificationsQuery(Guid? UserId) : IRequest<List<CertificationResponse>>;
public record GetCourseReviewsQuery(Guid CourseId) : IRequest<List<CourseReviewResponse>>;
public record CreateCourseReviewCommand(Guid StudentId, CreateCourseReviewRequest Request) : IRequest<CourseReviewResponse>;

public class GetCourseLessonsQueryHandler : IRequestHandler<GetCourseLessonsQuery, List<CourseLessonResponse>>
{
    private readonly ICourseLessonRepository _repo;
    private readonly IMapper _mapper;
    public GetCourseLessonsQueryHandler(ICourseLessonRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CourseLessonResponse>> Handle(GetCourseLessonsQuery r, CancellationToken ct) => _mapper.Map<List<CourseLessonResponse>>(await _repo.GetByCourseIdAsync(r.CourseId));
}

public class CreateCourseLessonCommandHandler : IRequestHandler<CreateCourseLessonCommand, CourseLessonResponse>
{
    private readonly ICourseLessonRepository _repo;
    private readonly IMapper _mapper;
    public CreateCourseLessonCommandHandler(ICourseLessonRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<CourseLessonResponse> Handle(CreateCourseLessonCommand r, CancellationToken ct)
    {
        var lesson = new CourseLesson { CourseId = r.Request.CourseId, Title = r.Request.Title, Description = r.Request.Description, VideoUrl = r.Request.VideoUrl ?? "", Duration = r.Request.Duration, DisplayOrder = r.Request.DisplayOrder, IsFree = r.Request.IsFree, Type = r.Request.Type };
        await _repo.AddAsync(lesson, ct);
        return _mapper.Map<CourseLessonResponse>(lesson);
    }
}

public class GetLessonProgressQueryHandler : IRequestHandler<GetLessonProgressQuery, List<LessonProgressResponse>>
{
    private readonly ILessonProgressRepository _repo;
    private readonly IMapper _mapper;
    public GetLessonProgressQueryHandler(ILessonProgressRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<LessonProgressResponse>> Handle(GetLessonProgressQuery r, CancellationToken ct) => _mapper.Map<List<LessonProgressResponse>>(await _repo.GetByEnrollmentAsync(r.CourseId, r.UserId.ToString()));
}

public class UpdateLessonProgressCommandHandler : IRequestHandler<UpdateLessonProgressCommand, Unit>
{
    private readonly ILessonProgressRepository _repo;
    public UpdateLessonProgressCommandHandler(ILessonProgressRepository repo) => _repo = repo;
    public async Task<Unit> Handle(UpdateLessonProgressCommand r, CancellationToken ct)
    {
        var progress = await _repo.GetProgressAsync(r.Request.LessonId, r.UserId.ToString());
        if (progress == null) { progress = new LessonProgress { LessonId = r.Request.LessonId, StudentId = r.UserId.ToString(), IsCompleted = r.Request.IsCompleted, WatchTime = r.Request.WatchTime, LastPosition = r.Request.LastPosition ?? "", CompletedAt = r.Request.IsCompleted ? DateTime.UtcNow : null }; await _repo.AddAsync(progress, ct); }
        else { progress.IsCompleted = r.Request.IsCompleted; progress.WatchTime = r.Request.WatchTime; progress.LastPosition = r.Request.LastPosition ?? progress.LastPosition; if (r.Request.IsCompleted && progress.CompletedAt == null) progress.CompletedAt = DateTime.UtcNow; await _repo.UpdateAsync(progress, ct); }
        return Unit.Value;
    }
}

public class GetLearningPathsQueryHandler : IRequestHandler<GetLearningPathsQuery, List<LearningPathResponse>>
{
    private readonly ILearningPathRepository _repo;
    private readonly IMapper _mapper;
    public GetLearningPathsQueryHandler(ILearningPathRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<LearningPathResponse>> Handle(GetLearningPathsQuery r, CancellationToken ct)
    {
        List<LearningPath> paths;
        if (r.Published == true) paths = await _repo.GetPublishedPathsAsync();
        else if (r.Featured == true) paths = await _repo.GetFeaturedPathsAsync(r.PageSize);
        else paths = (await _repo.GetAllAsync(ct)).ToList();
        return _mapper.Map<List<LearningPathResponse>>(paths);
    }
}

public class CreateLearningPathCommandHandler : IRequestHandler<CreateLearningPathCommand, LearningPathResponse>
{
    private readonly ILearningPathRepository _repo;
    private readonly IMapper _mapper;
    public CreateLearningPathCommandHandler(ILearningPathRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<LearningPathResponse> Handle(CreateLearningPathCommand r, CancellationToken ct)
    {
        var path = new LearningPath { InstructorId = r.InstructorId.ToString(), Title = r.Request.Title, Description = r.Request.Description, ThumbnailUrl = r.Request.ThumbnailUrl ?? "", IsFeatured = r.Request.IsFeatured, IsPublished = true, Status = LearningPathStatus.Published, Price = r.Request.Price, IsFree = r.Request.IsFree, Category = r.Request.Category ?? "" };
        await _repo.AddAsync(path, ct);
        return _mapper.Map<LearningPathResponse>(path);
    }
}

public class GetCertificationsQueryHandler : IRequestHandler<GetCertificationsQuery, List<CertificationResponse>>
{
    private readonly ICertificationRepository _repo;
    private readonly IMapper _mapper;
    public GetCertificationsQueryHandler(ICertificationRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CertificationResponse>> Handle(GetCertificationsQuery r, CancellationToken ct)
    {
        var certs = r.UserId.HasValue ? await _repo.GetByUserIdAsync(r.UserId.Value.ToString()) : (await _repo.GetAllAsync(ct)).ToList();
        return _mapper.Map<List<CertificationResponse>>(certs);
    }
}

public class GetCourseReviewsQueryHandler : IRequestHandler<GetCourseReviewsQuery, List<CourseReviewResponse>>
{
    private readonly ICourseReviewRepository _repo;
    private readonly IMapper _mapper;
    public GetCourseReviewsQueryHandler(ICourseReviewRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CourseReviewResponse>> Handle(GetCourseReviewsQuery r, CancellationToken ct) => _mapper.Map<List<CourseReviewResponse>>(await _repo.GetByCourseIdAsync(r.CourseId));
}

public class CreateCourseReviewCommandHandler : IRequestHandler<CreateCourseReviewCommand, CourseReviewResponse>
{
    private readonly ICourseReviewRepository _repo;
    private readonly ICourseEnrollmentRepository _enrollmentRepo;
    private readonly IMapper _mapper;
    public CreateCourseReviewCommandHandler(ICourseReviewRepository repo, ICourseEnrollmentRepository enrollmentRepo, IMapper mapper) { _repo = repo; _enrollmentRepo = enrollmentRepo; _mapper = mapper; }
    public async Task<CourseReviewResponse> Handle(CreateCourseReviewCommand r, CancellationToken ct)
    {
        var isEnrolled = await _enrollmentRepo.IsEnrolledAsync(r.Request.CourseId, r.StudentId.ToString());
        var review = new CourseReview { CourseId = r.Request.CourseId, StudentId = r.StudentId.ToString(), Title = r.Request.Title, Content = r.Request.Content, Rating = r.Request.Rating, IsVerifiedPurchase = isEnrolled };
        await _repo.AddAsync(review, ct);
        return _mapper.Map<CourseReviewResponse>(review);
    }
}
