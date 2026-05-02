using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DEPI.Domain.Entities.Recruitment;
using DEPI.Domain.Interfaces;

namespace DEPI.Application.Repositories.Recruitment;

public interface IJobRepository : IRepository<Job>
{
    Task<List<Job>> GetActiveJobsAsync();
    Task<List<Job>> GetByCompanyIdAsync(Guid companyId);
    Task<List<Job>> GetByOwnerIdAsync(string ownerId);
    Task<List<Job>> SearchJobsAsync(string searchTerm, JobType? type = null, string? location = null);
    Task<List<Job>> GetFeaturedJobsAsync(int count);
    Task<List<Job>> GetRecommendedForUserAsync(string userId, int count);
    Task IncrementViewsAsync(Guid jobId);
    Task IncrementApplicationsAsync(Guid jobId);
}

public interface IJobCategoryRepository : IRepository<JobCategory>
{
    Task<List<JobCategory>> GetActiveCategoriesAsync();
    Task<List<JobCategory>> GetRootCategoriesAsync();
    Task<List<JobCategory>> GetSubCategoriesAsync(Guid parentId);
}

public interface IJobApplicationRepository : IRepository<JobApplication>
{
    Task<List<JobApplication>> GetByJobIdAsync(Guid jobId);
    Task<List<JobApplication>> GetByApplicantIdAsync(string applicantId);
    Task<List<JobApplication>> GetShortlistedAsync(Guid jobId);
    Task<JobApplication?> GetWithDetailsAsync(Guid applicationId);
    Task<List<JobApplication>> GetRecentApplicationsAsync(string userId, int count);
}

public interface IJobSkillRepository : IRepository<JobSkill> { }

public interface IJobQuestionRepository : IRepository<JobQuestion>
{
    Task<List<JobQuestion>> GetByJobIdAsync(Guid jobId);
}