using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

namespace DEPI.Domain.Entities.Learning;

public class Course : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public Guid InstructorId { get; set; }
    public CourseLevel Level { get; set; } = CourseLevel.Beginner;
    public CourseStatus Status { get; set; } = CourseStatus.Draft;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public bool IsFree { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
    public int Duration { get; set; }
    public int LessonsCount { get; set; }
    public int StudentsCount { get; set; }
    public int Rating { get; set; }
    public int ReviewsCount { get; set; }
    public int CompletionRate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string WhatYouLearn { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public virtual User Instructor { get; set; } = null!;
    public virtual ICollection<CourseLesson> Lessons { get; set; } = new List<CourseLesson>();
    public virtual ICollection<CourseEnrollment> Enrollments { get; set; } = new List<CourseEnrollment>();
    public virtual ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
}

public class CourseLesson : AuditableEntity
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsFree { get; set; }
    public LessonType Type { get; set; } = LessonType.Video;

    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<LessonProgress> Progress { get; set; } = new List<LessonProgress>();
}

public class CourseEnrollment : AuditableEntity
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public decimal PricePaid { get; set; }
    public string Currency { get; set; } = "USD";
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    public int Progress { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? LastAccessedAt { get; set; }
    public int LessonsCompleted { get; set; }

    public virtual Course Course { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
}

public class LessonProgress : AuditableEntity
{
    public Guid LessonId { get; set; }
    public Guid StudentId { get; set; }
    public bool IsCompleted { get; set; }
    public int WatchTime { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int AttemptsCount { get; set; }
    public string LastPosition { get; set; } = string.Empty;

    public virtual CourseLesson Lesson { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
}

public class LearningPath : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public Guid InstructorId { get; set; }
    public LearningPathStatus Status { get; set; } = LearningPathStatus.Draft;
    public bool IsFeatured { get; set; }
    public bool IsPublished { get; set; }
    public int TotalDuration { get; set; }
    public int CoursesCount { get; set; }
    public int StudentsCount { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public string Category { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public virtual User Instructor { get; set; } = null!;
    public virtual ICollection<LearningPathCourse> Courses { get; set; } = new List<LearningPathCourse>();
}

public class LearningPathCourse : AuditableEntity
{
    public Guid LearningPathId { get; set; }
    public Guid CourseId { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsRequired { get; set; } = true;

    public virtual LearningPath LearningPath { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}

public class Certification : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IssuingOrganization { get; set; } = string.Empty;
    public string CredentialUrl { get; set; } = string.Empty;
    public string CredentialId { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public bool IsExpired { get; set; }
    public string BadgeUrl { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public string Category { get; set; } = string.Empty;
}

public class CourseReview : AuditableEntity
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsVerifiedPurchase { get; set; }

    public virtual Course Course { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
}

public enum CourseLevel
{
    Beginner,
    Intermediate,
    Advanced,
    Expert
}

public enum CourseStatus
{
    Draft,
    UnderReview,
    Published,
    Archived,
    Rejected
}

public enum EnrollmentStatus
{
    Active,
    Completed,
    Dropped,
    Expired
}

public enum LessonType
{
    Video,
    Article,
    Quiz,
    Assignment,
    LiveSession
}

public enum LearningPathStatus
{
    Draft,
    Published,
    Archived
}