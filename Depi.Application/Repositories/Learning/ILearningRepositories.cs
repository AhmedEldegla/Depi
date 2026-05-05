using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Learning;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Learning;

public interface ICourseRepository : IRepository<Course>
{
    Task<List<Course>> GetPublishedCoursesAsync();
    Task<List<Course>> GetByInstructorIdAsync(Guid instructorId);
    Task<List<Course>> GetFeaturedCoursesAsync(int count);
    Task<List<Course>> GetByCategoryAsync(string category, int count);
    Task<List<Course>> SearchCoursesAsync(string searchTerm);
}

public interface ICourseLessonRepository : IRepository<CourseLesson>
{
    Task<List<CourseLesson>> GetByCourseIdAsync(Guid courseId);
    Task<List<CourseLesson>> GetFreeLessonsAsync(Guid courseId);
}

public interface ICourseEnrollmentRepository : IRepository<CourseEnrollment>
{
    Task<List<CourseEnrollment>> GetByStudentIdAsync(Guid studentId);
    Task<List<CourseEnrollment>> GetActiveEnrollmentsAsync(Guid studentId);
    Task<CourseEnrollment?> GetEnrollmentAsync(Guid courseId, Guid studentId);
    Task<bool> IsEnrolledAsync(Guid courseId, Guid studentId);
}

public interface ILearningPathRepository : IRepository<LearningPath>
{
    Task<List<LearningPath>> GetPublishedPathsAsync();
    Task<List<LearningPath>> GetFeaturedPathsAsync(int count);
}

public interface ILearningPathCourseRepository : IRepository<LearningPathCourse>
{
    Task<List<LearningPathCourse>> GetByPathIdAsync(Guid pathId);
}

public interface ICertificationRepository : IRepository<Certification>
{
    Task<List<Certification>> GetByUserIdAsync(Guid userId);
    Task<List<Certification>> GetExpiringSoonAsync(int days);
    Task<List<Certification>> GetExpiredCertificationsAsync();
}

public interface ICourseReviewRepository : IRepository<CourseReview>
{
    Task<List<CourseReview>> GetByCourseIdAsync(Guid courseId);
    Task<List<CourseReview>> GetByStudentIdAsync(Guid studentId);
    Task<decimal> GetAverageRatingAsync(Guid courseId);
}

public interface ILessonProgressRepository : IRepository<LessonProgress>
{
    Task<LessonProgress?> GetProgressAsync(Guid lessonId, Guid studentId);
    Task<List<LessonProgress>> GetByEnrollmentAsync(Guid courseId, Guid studentId);
    Task<int> GetCompletedCountAsync(Guid courseId, Guid studentId);
}