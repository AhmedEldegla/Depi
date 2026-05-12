using DEPI.Domain.Entities.Learning;

namespace DEPI.Application.DTOs.Learning;

public class CourseResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public CourseLevel Level { get; set; }
    public CourseStatus Status { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public bool IsFeatured { get; set; }
    public int Duration { get; set; }
    public int LessonsCount { get; set; }
    public int StudentsCount { get; set; }
    public decimal Rating { get; set; }
    public int ReviewsCount { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CourseLessonResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFree { get; set; }
    public LessonType Type { get; set; }
}

public class LessonProgressResponse
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public bool IsCompleted { get; set; }
    public int WatchTime { get; set; }
    public int AttemptsCount { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class LearningPathResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
    public int TotalDuration { get; set; }
    public int CoursesCount { get; set; }
    public int StudentsCount { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CertificationResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IssuingOrganization { get; set; } = string.Empty;
    public string CredentialUrl { get; set; } = string.Empty;
    public string CredentialId { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsVerified { get; set; }
    public string BadgeUrl { get; set; } = string.Empty;
}

public class CourseReviewResponse
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public int HelpfulnessScore { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record CreateCourseRequest(
    string Title,
    string Description,
    string? ThumbnailUrl,
    string? VideoUrl,
    CourseLevel Level,
    decimal Price,
    bool IsFree,
    string? Category,
    string? Tags
);

public record EnrollCourseRequest(Guid CourseId);