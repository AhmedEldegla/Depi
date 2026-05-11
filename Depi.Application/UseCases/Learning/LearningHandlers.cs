using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.Repositories.Learning;
using DEPI.Domain.Entities.Learning;
using MediatR;
using DEPI.Application.DTOs.Learning;

namespace DEPI.Application.UseCases.Learning;



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
