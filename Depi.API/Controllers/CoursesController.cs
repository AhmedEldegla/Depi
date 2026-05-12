using DEPI.Application.DTOs.Learning;
using DEPI.Application.UseCases.Learning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // =========================
    // Courses
    // =========================

    [HttpGet]
    public async Task<IActionResult> GetCourses(
        [FromQuery] bool? published,
        [FromQuery] bool? featured,
        [FromQuery] string? category,
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(
            new GetCoursesQuery(
                published,
                featured,
                category,
                searchTerm,
                page,
                pageSize));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(
        Guid instructorId,
        [FromBody] CreateCourseRequest request)
    {
        var result = await _mediator.Send(
            new CreateCourseCommand(instructorId, request));

        return Ok(result);
    }

    // =========================
    // Enrollment
    // =========================

    [HttpPost("enroll")]
    public async Task<IActionResult> EnrollCourse(
        Guid studentId,
        [FromBody] EnrollCourseRequest request)
    {
        var result = await _mediator.Send(
            new EnrollCourseCommand(studentId, request));

        return Ok(result);
    }

    // =========================
    // Lessons
    // =========================

    [HttpGet("{courseId}/lessons")]
    public async Task<IActionResult> GetLessons(Guid courseId)
    {
        var result = await _mediator.Send(
            new GetCourseLessonsQuery(courseId));

        return Ok(result);
    }

    [HttpPost("lessons")]
    public async Task<IActionResult> CreateLesson(
        Guid instructorId,
        [FromBody] CreateCourseLessonRequest request)
    {
        var result = await _mediator.Send(
            new CreateCourseLessonCommand(instructorId, request));

        return Ok(result);
    }

    // =========================
    // Lesson Progress
    // =========================

    [HttpGet("{courseId}/progress")]
    public async Task<IActionResult> GetProgress(
        Guid userId,
        Guid courseId)
    {
        var result = await _mediator.Send(
            new GetLessonProgressQuery(userId, courseId));

        return Ok(result);
    }

    [HttpPut("progress")]
    public async Task<IActionResult> UpdateProgress(
        Guid userId,
        [FromBody] UpdateLessonProgressRequest request)
    {
        await _mediator.Send(
            new UpdateLessonProgressCommand(userId, request));

        return NoContent();
    }

    // =========================
    // Learning Paths
    // =========================

    [HttpGet("learning-paths")]
    public async Task<IActionResult> GetLearningPaths(
        [FromQuery] bool? published,
        [FromQuery] bool? featured,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(
            new GetLearningPathsQuery(
                published,
                featured,
                page,
                pageSize));

        return Ok(result);
    }

    [HttpPost("learning-paths")]
    public async Task<IActionResult> CreateLearningPath(
        Guid instructorId,
        [FromBody] CreateLearningPathRequest request)
    {
        var result = await _mediator.Send(
            new CreateLearningPathCommand(instructorId, request));

        return Ok(result);
    }

    // =========================
    // Certifications
    // =========================

    [HttpGet("certifications")]
    public async Task<IActionResult> GetCertifications(Guid? userId)
    {
        var result = await _mediator.Send(
            new GetCertificationsQuery(userId));

        return Ok(result);
    }

    // =========================
    // Reviews
    // =========================

    [HttpGet("{courseId}/reviews")]
    public async Task<IActionResult> GetReviews(Guid courseId)
    {
        var result = await _mediator.Send(
            new GetCourseReviewsQuery(courseId));

        return Ok(result);
    }

    [HttpPost("reviews")]
    public async Task<IActionResult> CreateReview(
        Guid studentId,
        [FromBody] CreateCourseReviewRequest request)
    {
        var result = await _mediator.Send(
            new CreateCourseReviewCommand(studentId, request));

        return Ok(result);
    }
}