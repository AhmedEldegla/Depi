using DEPI.Application.Repositories.Learning;
using DEPI.Domain.Entities.Learning;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Course>> GetPublishedCoursesAsync()
    {
        return await _dbSet.Where(c => c.IsPublished).OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<List<Course>> GetByInstructorIdAsync(Guid instructorId)
    {
        return await _dbSet.Where(c => c.InstructorId == instructorId).ToListAsync();
    }

    public async Task<List<Course>> GetFeaturedCoursesAsync(int count)
    {
        return await _dbSet.Where(c => c.IsFeatured).OrderByDescending(c => c.CreatedAt).Take(count).ToListAsync();
    }

    public async Task<List<Course>> GetByCategoryAsync(string category, int count)
    {
        return await _dbSet.Where(c => c.Category == category).OrderByDescending(c => c.CreatedAt).Take(count).ToListAsync();
    }

    public async Task<List<Course>> SearchCoursesAsync(string searchTerm)
    {
        return await _dbSet.Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)).ToListAsync();
    }
}

public class CourseLessonRepository : Repository<CourseLesson>, ICourseLessonRepository
{
    public CourseLessonRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CourseLesson>> GetByCourseIdAsync(Guid courseId)
    {
        return await _dbSet.Where(l => l.CourseId == courseId).OrderBy(l => l.DisplayOrder).ToListAsync();
    }

    public async Task<List<CourseLesson>> GetFreeLessonsAsync(Guid courseId)
    {
        return await _dbSet.Where(l => l.CourseId == courseId && l.IsFree).ToListAsync();
    }
}

public class CourseEnrollmentRepository : Repository<CourseEnrollment>, ICourseEnrollmentRepository
{
    public CourseEnrollmentRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CourseEnrollment>> GetByStudentIdAsync(Guid studentId)
    {
        return await _dbSet.Where(e => e.StudentId == studentId).ToListAsync();
    }

    public async Task<List<CourseEnrollment>> GetActiveEnrollmentsAsync(Guid studentId)
    {
        return await _dbSet.Where(e => e.StudentId == studentId && e.Status == EnrollmentStatus.Active).ToListAsync();
    }

    public async Task<CourseEnrollment?> GetEnrollmentAsync(Guid courseId, Guid studentId)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.CourseId == courseId && e.StudentId == studentId);
    }

    public async Task<bool> IsEnrolledAsync(Guid courseId, Guid studentId)
    {
        return await _dbSet.AnyAsync(e => e.CourseId == courseId && e.StudentId == studentId && e.Status == EnrollmentStatus.Active);
    }
}

public class LearningPathRepository : Repository<LearningPath>, ILearningPathRepository
{
    public LearningPathRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<LearningPath>> GetPublishedPathsAsync()
    {
        return await _dbSet.Where(p => p.IsPublished).OrderByDescending(p => p.CreatedAt).ToListAsync();
    }

    public async Task<List<LearningPath>> GetFeaturedPathsAsync(int count)
    {
        return await _dbSet.Where(p => p.IsFeatured).OrderByDescending(p => p.CreatedAt).Take(count).ToListAsync();
    }
}

public class LearningPathCourseRepository : Repository<LearningPathCourse>, ILearningPathCourseRepository
{
    public LearningPathCourseRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<LearningPathCourse>> GetByPathIdAsync(Guid pathId)
    {
        return await _dbSet.Where(lpc => lpc.LearningPathId == pathId).OrderBy(lpc => lpc.DisplayOrder).ToListAsync();
    }
}

public class CertificationRepository : Repository<Certification>, ICertificationRepository
{
    public CertificationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Certification>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(c => c.CreatedBy == userId).ToListAsync();
    }

    public async Task<List<Certification>> GetExpiringSoonAsync(int days)
    {
        var expiryDate = DateTime.UtcNow.AddDays(days);
        return await _dbSet.Where(c => c.ExpiryDate != null && c.ExpiryDate <= expiryDate && c.ExpiryDate >= DateTime.UtcNow).ToListAsync();
    }

    public async Task<List<Certification>> GetExpiredCertificationsAsync()
    {
        return await _dbSet.Where(c => c.ExpiryDate != null && c.ExpiryDate < DateTime.UtcNow).ToListAsync();
    }
}

public class CourseReviewRepository : Repository<CourseReview>, ICourseReviewRepository
{
    public CourseReviewRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<CourseReview>> GetByCourseIdAsync(Guid courseId)
    {
        return await _dbSet.Where(r => r.CourseId == courseId).OrderByDescending(r => r.CreatedAt).ToListAsync();
    }

    public async Task<List<CourseReview>> GetByStudentIdAsync(Guid studentId)
    {
        return await _dbSet.Where(r => r.StudentId == studentId).ToListAsync();
    }

    public async Task<decimal> GetAverageRatingAsync(Guid courseId)
    {
        var ratings = await _dbSet.Where(r => r.CourseId == courseId).Select(r => (decimal)r.Rating).ToListAsync();
        return ratings.Count > 0 ? ratings.Average() : 0m;
    }
}

public class LessonProgressRepository : Repository<LessonProgress>, ILessonProgressRepository
{
    public LessonProgressRepository(ApplicationDbContext context) : base(context) { }

    public async Task<LessonProgress?> GetProgressAsync(Guid lessonId, Guid studentId)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.LessonId == lessonId && p.StudentId == studentId);
    }

    public async Task<List<LessonProgress>> GetByEnrollmentAsync(Guid courseId, Guid studentId)
    {
        return new List<LessonProgress>();
    }

    public async Task<int> GetCompletedCountAsync(Guid courseId, Guid studentId)
    {
        return 0;
    }
}
