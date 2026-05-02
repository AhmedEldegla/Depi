using DEPI.Application.Repositories.Recruitment;
using DEPI.Domain.Entities.Recruitment;
using Microsoft.EntityFrameworkCore;

namespace DEPI.Infrastructure.Persistence.Repositories;

public class JobRepository : Repository<Job>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<Job>> GetActiveJobsAsync()
    {
        return await _dbSet.Where(j => j.Status == JobStatus.Active).OrderByDescending(j => j.CreatedAt).ToListAsync();
    }

    public async Task<List<Job>> GetByCompanyIdAsync(Guid companyId)
    {
        return await _dbSet.Where(j => j.CompanyId == companyId).ToListAsync();
    }

    public async Task<List<Job>> GetByOwnerIdAsync(string ownerId)
    {
        return await _dbSet.Where(j => j.OwnerId == ownerId).ToListAsync();
    }

    public async Task<List<Job>> SearchJobsAsync(string searchTerm, JobType? type = null, string? location = null)
    {
        var query = _dbSet.Where(j => j.Title.Contains(searchTerm) || j.Description.Contains(searchTerm));

        if (type.HasValue)
            query = query.Where(j => j.Type == type.Value);

        if (!string.IsNullOrWhiteSpace(location))
            query = query.Where(j => j.Location.Contains(location));

        return await query.ToListAsync();
    }

    public async Task<List<Job>> GetFeaturedJobsAsync(int count)
    {
        return await _dbSet.Where(j => j.IsFeatured).OrderByDescending(j => j.CreatedAt).Take(count).ToListAsync();
    }

    public async Task<List<Job>> GetRecommendedForUserAsync(string userId, int count)
    {
        return await _dbSet
            .Where(j => j.Status == JobStatus.Active)
            .OrderByDescending(j => j.MatchScore)
            .Take(count)
            .ToListAsync();
    }

    public async Task IncrementViewsAsync(Guid jobId)
    {
        var job = await _dbSet.FindAsync(jobId);
        if (job != null)
            job.ViewsCount++;
    }

    public async Task IncrementApplicationsAsync(Guid jobId)
    {
        var job = await _dbSet.FindAsync(jobId);
        if (job != null)
            job.ApplicationsCount++;
    }
}

public class JobCategoryRepository : Repository<JobCategory>, IJobCategoryRepository
{
    public JobCategoryRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<JobCategory>> GetActiveCategoriesAsync()
    {
        return await _dbSet.Where(c => c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
    }

    public async Task<List<JobCategory>> GetRootCategoriesAsync()
    {
        return await _dbSet.Where(c => c.ParentCategoryId == null && c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
    }

    public async Task<List<JobCategory>> GetSubCategoriesAsync(Guid parentId)
    {
        return await _dbSet.Where(c => c.ParentCategoryId == parentId && c.IsActive).OrderBy(c => c.DisplayOrder).ToListAsync();
    }
}

public class JobApplicationRepository : Repository<JobApplication>, IJobApplicationRepository
{
    public JobApplicationRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<JobApplication>> GetByJobIdAsync(Guid jobId)
    {
        return await _dbSet.Where(a => a.JobId == jobId).OrderByDescending(a => a.CreatedAt).ToListAsync();
    }

    public async Task<List<JobApplication>> GetByApplicantIdAsync(string applicantId)
    {
        return await _dbSet.Where(a => a.ApplicantId == applicantId).ToListAsync();
    }

    public async Task<List<JobApplication>> GetShortlistedAsync(Guid jobId)
    {
        return await _dbSet.Where(a => a.JobId == jobId && a.IsShortlisted).ToListAsync();
    }

    public async Task<JobApplication?> GetWithDetailsAsync(Guid applicationId)
    {
        return await _dbSet
            .Include(a => a.Job)
            .Include(a => a.Applicant)
            .Include(a => a.Attachments)
            .FirstOrDefaultAsync(a => a.Id == applicationId);
    }

    public async Task<List<JobApplication>> GetRecentApplicationsAsync(string userId, int count)
    {
        return await _dbSet.Where(a => a.ApplicantId == userId).OrderByDescending(a => a.CreatedAt).Take(count).ToListAsync();
    }
}

public class JobSkillRepository : Repository<JobSkill>, IJobSkillRepository
{
    public JobSkillRepository(ApplicationDbContext context) : base(context) { }
}

public class JobQuestionRepository : Repository<JobQuestion>, IJobQuestionRepository
{
    public JobQuestionRepository(ApplicationDbContext context) : base(context) { }

    public async Task<List<JobQuestion>> GetByJobIdAsync(Guid jobId)
    {
        return await _dbSet.Where(q => q.JobId == jobId).OrderBy(q => q.DisplayOrder).ToListAsync();
    }
}
