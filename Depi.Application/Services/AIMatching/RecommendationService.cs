using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.AIMatching;
using DEPI.Application.Repositories.Community;
using DEPI.Application.Repositories.Companies;
using DEPI.Application.Repositories.Learning;
using DEPI.Application.Repositories.Profiles;
using DEPI.Application.Repositories.Projects;
using DEPI.Application.Repositories.Recruitment;
using DEPI.Domain.Entities.AIMatching;
using DEPI.Domain.Entities.Community;
using DEPI.Domain.Entities.Learning;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Recruitment;
using DEPI.Domain.Enums;
using IProjectRepository = DEPI.Application.Interfaces.IProjectRepository;
using RecommendationType = DEPI.Domain.Entities.AIMatching.RecommendationType;

namespace DEPI.Application.Services.AIMatching;

public class RecommendationService : IRecommendationService
{
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IJobRepository _jobRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ILearningPathRepository _learningPathRepository;
    private readonly IFreelancerProfileRepository _freelancerProfileRepository;
    private readonly IAIMatchingService _aiMatchingService;
    private readonly ICommunityPostRepository _postRepository;

    public RecommendationService(
        IRecommendationRepository recommendationRepository,
        IProjectRepository projectRepository,
        IJobRepository jobRepository,
        ICourseRepository courseRepository,
        ILearningPathRepository learningPathRepository,
        IFreelancerProfileRepository freelancerProfileRepository,
        IAIMatchingService aiMatchingService,
        ICommunityPostRepository postRepository)
    {
        _recommendationRepository = recommendationRepository;
        _projectRepository = projectRepository;
        _jobRepository = jobRepository;
        _courseRepository = courseRepository;
        _learningPathRepository = learningPathRepository;
        _freelancerProfileRepository = freelancerProfileRepository;
        _aiMatchingService = aiMatchingService;
        _postRepository = postRepository;
    }

    public async Task<List<RecommendationResult>> GetPersonalizedRecommendationsAsync(Guid userId)
    {
        var allRecommendations = new List<RecommendationResult>();

        allRecommendations.AddRange(await GetProjectRecommendationsForFreelancerAsync(userId));
        allRecommendations.AddRange(await GetJobRecommendationsForFreelancerAsync(userId));
        allRecommendations.AddRange(await GetCourseRecommendationsForUserAsync(userId));

        return allRecommendations
            .OrderByDescending(r => r.ConfidenceScore)
            .Take(20)
            .ToList();
    }

    public async Task<List<RecommendationResult>> GetProjectRecommendationsForFreelancerAsync(Guid freelancerId)
    {
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return new List<RecommendationResult>();

        var projects = await _projectRepository.GetAllProjectsAsync();
        var recommendations = new List<RecommendationResult>();

        foreach (var project in projects.Where(p => p.Status == ProjectStatus.Open))
        {
            var score = await _aiMatchingService.CalculateProjectMatchScoreAsync(project.Id, freelancerId);
            
            if (score >= 0.6m)
            {
                recommendations.Add(new RecommendationResult
                {
                    Id = Guid.NewGuid(),
                    Type = RecommendationType.Project,
                    TargetId = project.Id,
                    TargetName = project.Title,
                    Reason = GenerateProjectRecommendationReason(profile, project, score),
                    ConfidenceScore = score,
                    Context = $"Matches {score:P0} with your profile"
                });
            }
        }

        return recommendations
            .OrderByDescending(r => r.ConfidenceScore)
            .Take(10)
            .ToList();
    }

    public async Task<List<RecommendationResult>> GetJobRecommendationsForFreelancerAsync(Guid freelancerId)
    {
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return new List<RecommendationResult>();

        var jobs = await _jobRepository.GetActiveJobsAsync();
        var recommendations = new List<RecommendationResult>();

        foreach (var job in jobs)
        {
            var score = await _aiMatchingService.CalculateJobMatchScoreAsync(job.Id, freelancerId);
            
            if (score >= 0.5m)
            {
                recommendations.Add(new RecommendationResult
                {
                    Id = Guid.NewGuid(),
                    Type = RecommendationType.Job,
                    TargetId = job.Id,
                    TargetName = job.Title,
                    Reason = GenerateJobRecommendationReason(profile, job, score),
                    ConfidenceScore = score,
                    Context = $"{job.Company?.Name ?? "Company"} - {job.Type}"
                });
            }
        }

        return recommendations
            .OrderByDescending(r => r.ConfidenceScore)
            .Take(10)
            .ToList();
    }

    public async Task<List<RecommendationResult>> GetCourseRecommendationsForUserAsync(Guid userId)
    {
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(userId);
        if (profile == null) return new List<RecommendationResult>();

        var courses = await _courseRepository.GetPublishedCoursesAsync();
        var recommendations = new List<RecommendationResult>();

        foreach (var course in courses)
        {
            var relevanceScore = CalculateCourseRelevance(profile, course);
            
            if (relevanceScore >= 0.4m)
            {
                recommendations.Add(new RecommendationResult
                {
                    Id = Guid.NewGuid(),
                    Type = RecommendationType.Course,
                    TargetId = course.Id,
                    TargetName = course.Title,
                    Reason = $"Perfect for {profile.ExperienceLevel} level - {course.Level} difficulty",
                    ConfidenceScore = relevanceScore,
                    Context = $"{course.Category} - {course.Duration}min"
                });
            }
        }

        return recommendations
            .OrderByDescending(r => r.ConfidenceScore)
            .Take(5)
            .ToList();
    }

    public async Task RecordRecommendationClickAsync(Guid recommendationId)
    {
        var recommendations = await _recommendationRepository.GetByUserIdAsync(Guid.Empty);
        var recommendation = recommendations.FirstOrDefault(r => r.Id == recommendationId);
        
        if (recommendation != null)
        {
            recommendation.IsClicked = true;
            recommendation.ClickedAt = DateTime.UtcNow;
            await _recommendationRepository.UpdateAsync(recommendation);
        }
    }

    public async Task<decimal> GetRecommendationConfidenceAsync(Guid userId, DEPI.Domain.Entities.AIMatching.RecommendationType type)
    {
        var recommendations = await _recommendationRepository.GetByTypeAsync(userId, type);
        
        if (!recommendations.Any()) return 0;

        var avgConfidence = recommendations.Average(r => r.ConfidenceScore);
        var clickRate = recommendations.Count(r => r.IsClicked) / (decimal)recommendations.Count();

        return (avgConfidence * 0.7m) + (clickRate * 0.3m);
    }

    private string GenerateProjectRecommendationReason(UserProfile profile, Project project, decimal score)
    {
        var reasons = new List<string>();

        if (profile.ExperienceLevel.Contains("Expert") && score > 0.8m)
            reasons.Add("matches your expert-level experience");

        if (profile.IsAvailable)
            reasons.Add("your availability aligns with the timeline");

        if (!string.IsNullOrEmpty(profile.CountryName) && !string.IsNullOrEmpty(project.Country))
        {
            if (profile.CountryName.Equals(project.Country, StringComparison.OrdinalIgnoreCase))
                reasons.Add("location matches your profile");
        }

        if (reasons.Any())
            return string.Join(", ", reasons);

        return $"Strong match ({score:P0} compatibility) based on your skills and experience";
    }

    private string GenerateJobRecommendationReason(UserProfile profile, Job job, decimal score)
    {
        var reasons = new List<string>();

        if (job.Type.ToString().Equals(profile.ExperienceLevel, StringComparison.OrdinalIgnoreCase))
            reasons.Add("perfect for your experience level");

        if (job.IsRemote)
            reasons.Add("remote position matches your preferences");

        if (!string.IsNullOrEmpty(profile.CountryName) && !string.IsNullOrEmpty(job.Location))
        {
            if (job.Location.Contains(profile.CountryName) || profile.CountryName.Contains(job.Location))
                reasons.Add("location match");
        }

        if (reasons.Any())
            return string.Join(", ", reasons);

        return $"Good match ({score:P0}) based on your qualifications";
    }

    private decimal CalculateCourseRelevance(UserProfile profile, Course course)
    {
        var score = 0.5m;

        var courseLevel = course.Level.ToString();
        var profileLevel = profile.ExperienceLevel;

        if (courseLevel.Equals(profileLevel, StringComparison.OrdinalIgnoreCase))
            score += 0.2m;

        if (course.Category?.Contains(profile.ExperienceLevel) == true)
            score += 0.1m;

        if (course.IsFree)
            score += 0.1m;

        return Math.Min(score, 1.0m);
    }
}