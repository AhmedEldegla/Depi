using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Learning;
using DEPI.Domain.Entities.Learning;
using MediatR;

namespace DEPI.Application.UseCases.Learning;

public class CourseResponse { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string ThumbnailUrl { get; set; } = string.Empty; public CourseLevel Level { get; set; } public CourseStatus Status { get; set; } public decimal Price { get; set; } public bool IsFree { get; set; } public bool IsFeatured { get; set; } public int Duration { get; set; } public int LessonsCount { get; set; } public int StudentsCount { get; set; } public decimal Rating { get; set; } public int ReviewsCount { get; set; } public string Category { get; set; } = string.Empty; public DateTime CreatedAt { get; set; } }
public class CourseLessonResponse { public Guid Id { get; set; } public Guid CourseId { get; set; } public string Title { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string VideoUrl { get; set; } = string.Empty; public int Duration { get; set; } public int DisplayOrder { get; set; } public bool IsFree { get; set; } public LessonType Type { get; set; } }
public class LessonProgressResponse { public Guid Id { get; set; } public Guid LessonId { get; set; } public bool IsCompleted { get; set; } public int WatchTime { get; set; } public int AttemptsCount { get; set; } public DateTime? CompletedAt { get; set; } }
public class LearningPathResponse { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string ThumbnailUrl { get; set; } = string.Empty; public bool IsFeatured { get; set; } public bool IsPublished { get; set; } public int TotalDuration { get; set; } public int CoursesCount { get; set; } public int StudentsCount { get; set; } public decimal Price { get; set; } public bool IsFree { get; set; } public string Category { get; set; } = string.Empty; public DateTime CreatedAt { get; set; } }
public class CertificationResponse { public Guid Id { get; set; } public string Title { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string IssuingOrganization { get; set; } = string.Empty; public string CredentialUrl { get; set; } = string.Empty; public string CredentialId { get; set; } = string.Empty; public DateTime IssueDate { get; set; } public DateTime? ExpiryDate { get; set; } public bool IsVerified { get; set; } public string BadgeUrl { get; set; } = string.Empty; }
public class CourseReviewResponse { public Guid Id { get; set; } public Guid CourseId { get; set; } public string Title { get; set; } = string.Empty; public string Content { get; set; } = string.Empty; public int Rating { get; set; } public bool IsVerifiedPurchase { get; set; } public int HelpfulnessScore { get; set; } public DateTime CreatedAt { get; set; } }

public record CreateCourseRequest(string Title, string Description, string? ThumbnailUrl, string? VideoUrl, CourseLevel Level, decimal Price, bool IsFree, string? Category, string? Tags);
public record EnrollCourseRequest(Guid CourseId);

public record CreateCourseCommand(Guid InstructorId, CreateCourseRequest Request) : IRequest<CourseResponse>;
public record GetCoursesQuery(bool? Published, bool? Featured, string? Category, string? SearchTerm, int Page, int PageSize) : IRequest<List<CourseResponse>>;
public record EnrollCourseCommand(Guid StudentId, EnrollCourseRequest Request) : IRequest<Guid>;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseResponse>
{
    private readonly ICourseRepository _repo;
    private readonly IMapper _mapper;
    public CreateCourseCommandHandler(ICourseRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<CourseResponse> Handle(CreateCourseCommand r, CancellationToken ct)
    {
        var course = new Course { InstructorId = r.InstructorId, Title = r.Request.Title, Description = r.Request.Description, ThumbnailUrl = r.Request.ThumbnailUrl ?? "", VideoUrl = r.Request.VideoUrl ?? "", Level = r.Request.Level, Status = CourseStatus.Published, Price = r.Request.Price, IsFree = r.Request.IsFree, Category = r.Request.Category ?? "", Tags = r.Request.Tags ?? "" };
        await _repo.AddAsync(course, ct);
        return _mapper.Map<CourseResponse>(course);
    }
}

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseResponse>>
{
    private readonly ICourseRepository _repo;
    private readonly IMapper _mapper;
    public GetCoursesQueryHandler(ICourseRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }
    public async Task<List<CourseResponse>> Handle(GetCoursesQuery r, CancellationToken ct)
    {
        List<Course> courses;
        if (r.Featured == true) courses = await _repo.GetFeaturedCoursesAsync(r.PageSize);
        else if (!string.IsNullOrWhiteSpace(r.Category)) courses = await _repo.GetByCategoryAsync(r.Category, r.PageSize);
        else if (!string.IsNullOrWhiteSpace(r.SearchTerm)) courses = await _repo.SearchCoursesAsync(r.SearchTerm);
        else if (r.Published == true) courses = await _repo.GetPublishedCoursesAsync();
        else courses = (await _repo.GetAllAsync(ct)).ToList();
        return _mapper.Map<List<CourseResponse>>(courses);
    }
}

public class EnrollCourseCommandHandler : IRequestHandler<EnrollCourseCommand, Guid>
{
    private readonly ICourseEnrollmentRepository _repo;
    public EnrollCourseCommandHandler(ICourseEnrollmentRepository repo) => _repo = repo;
    public async Task<Guid> Handle(EnrollCourseCommand r, CancellationToken ct)
    {
        var exists = await _repo.IsEnrolledAsync(r.Request.CourseId, r.StudentId);
        if (exists) throw new InvalidOperationException(Errors.AlreadyExists("Enrollment"));
        var enrollment = new CourseEnrollment { CourseId = r.Request.CourseId, StudentId = r.StudentId, Status = EnrollmentStatus.Active };
        await _repo.AddAsync(enrollment, ct);
        return enrollment.Id;
    }
}
