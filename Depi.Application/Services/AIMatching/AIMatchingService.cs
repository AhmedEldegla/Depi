using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.AIMatching;
using DEPI.Application.Repositories.Profiles;
using DEPI.Application.Repositories.Projects;
using DEPI.Application.Repositories.Recruitment;
using DEPI.Application.Repositories.Reviews;
using DEPI.Domain.Entities.AIMatching;
using DEPI.Domain.Entities.Projects;
using DEPI.Domain.Entities.Recruitment;
using DEPI.Domain.Entities.Reviews;
using IProjectRepository = DEPI.Application.Interfaces.IProjectRepository;
using IReviewRepository = DEPI.Application.Interfaces.IReviewRepository;
using ISkillRepository = DEPI.Application.Interfaces.ISkillRepository;

namespace DEPI.Application.Services.AIMatching;

public class AIMatchingService : IAIMatchingService
{
    private readonly IProjectMatchRepository _projectMatchRepository;
    private readonly IJobMatchRepository _jobMatchRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IFreelancerProfileRepository _freelancerProfileRepository;
    private readonly IFreelancerSkillRepository _freelancerSkillRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IAIModelConfigRepository _configRepository;
    private readonly IFreelancerScoreRepository _freelancerScoreRepository;
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IScoringRuleRepository _scoringRuleRepository;

    public AIMatchingService(
        IProjectMatchRepository projectMatchRepository,
        IJobMatchRepository jobMatchRepository,
        IProjectRepository projectRepository,
        IJobRepository jobRepository,
        IFreelancerProfileRepository freelancerProfileRepository,
        IFreelancerSkillRepository freelancerSkillRepository,
        ISkillRepository skillRepository,
        IReviewRepository reviewRepository,
        IAIModelConfigRepository configRepository,
        IFreelancerScoreRepository freelancerScoreRepository,
        IRecommendationRepository recommendationRepository,
        IScoringRuleRepository scoringRuleRepository)
    {
        _projectMatchRepository = projectMatchRepository;
        _jobMatchRepository = jobMatchRepository;
        _projectRepository = projectRepository;
        _jobRepository = jobRepository;
        _freelancerProfileRepository = freelancerProfileRepository;
        _freelancerSkillRepository = freelancerSkillRepository;
        _skillRepository = skillRepository;
        _reviewRepository = reviewRepository;
        _configRepository = configRepository;
        _freelancerScoreRepository = freelancerScoreRepository;
        _recommendationRepository = recommendationRepository;
        _scoringRuleRepository = scoringRuleRepository;
    }

    public async Task<ProjectMatchResult> MatchFreelancersToProjectAsync(Guid projectId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
            throw new ArgumentException("Project not found", nameof(projectId));

        var freelancers = await _freelancerProfileRepository.GetAllAsync();
        var results = new List<(string FreelancerId, decimal Score)>();

        foreach (var freelancer in freelancers)
        {
            var score = await CalculateProjectMatchScoreAsync(projectId, freelancer.UserId.ToString());
            results.Add((freelancer.UserId.ToString(), score));
        }

        results = results.OrderByDescending(x => x.Score).ToList();
        var topMatch = results.FirstOrDefault();

        return new ProjectMatchResult
        {
            ProjectId = projectId,
            FreelancerId = topMatch.FreelancerId,
            OverallScore = topMatch.Score
        };
    }

    public async Task<JobMatchResult> MatchFreelancersToJobAsync(Guid jobId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        if (job == null)
            throw new ArgumentException("Job not found", nameof(jobId));

        var freelancers = await _freelancerProfileRepository.GetAllAsync();
        var results = new List<(string FreelancerId, decimal Score)>();

        foreach (var freelancer in freelancers)
        {
            var score = await CalculateJobMatchScoreAsync(jobId, freelancer.UserId.ToString());
            results.Add((freelancer.UserId.ToString(), score));
        }

        results = results.OrderByDescending(x => x.Score).ToList();
        var topMatch = results.FirstOrDefault();

        return new JobMatchResult
        {
            JobId = jobId,
            FreelancerId = topMatch.FreelancerId,
            OverallScore = topMatch.Score
        };
    }

    public async Task<List<ProjectMatchResult>> GetTopProjectMatchesAsync(Guid projectId, int count = 10)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
            throw new ArgumentException("Project not found", nameof(projectId));

        var freelancers = await _freelancerProfileRepository.GetAllAsync();
        var results = new List<ProjectMatchResult>();

        foreach (var freelancer in freelancers)
        {
            var score = await CalculateProjectMatchScoreAsync(projectId, freelancer.UserId.ToString());
            results.Add(new ProjectMatchResult
            {
                ProjectId = projectId,
                FreelancerId = freelancer.UserId.ToString(),
                OverallScore = score,
                SkillScore = await CalculateSkillScoreAsync(projectId, freelancer.UserId.ToString()),
                ExperienceScore = await CalculateExperienceScoreAsync(freelancer.UserId.ToString()),
                BudgetScore = await CalculateBudgetScoreAsync(projectId, freelancer.UserId.ToString()),
                AvailabilityScore = await CalculateAvailabilityScoreAsync(freelancer.UserId.ToString()),
                ReputationScore = await CalculateReputationScoreAsync(freelancer.UserId.ToString()),
                AIReasoning = await GenerateProjectMatchReasoningAsync(projectId, freelancer.UserId.ToString(), score)
            });
        }

        return results
            .OrderByDescending(x => x.OverallScore)
            .Take(count)
            .ToList();
    }

    public async Task<List<JobMatchResult>> GetTopJobMatchesAsync(Guid jobId, int count = 10)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        if (job == null)
            throw new ArgumentException("Job not found", nameof(jobId));

        var freelancers = await _freelancerProfileRepository.GetAllAsync();
        var results = new List<JobMatchResult>();

        foreach (var freelancer in freelancers)
        {
            var score = await CalculateJobMatchScoreAsync(jobId, freelancer.UserId.ToString());
            results.Add(new JobMatchResult
            {
                JobId = jobId,
                FreelancerId = freelancer.UserId.ToString(),
                OverallScore = score,
                SkillScore = await CalculateSkillScoreForJobAsync(jobId, freelancer.UserId.ToString()),
                ExperienceScore = await CalculateExperienceScoreAsync(freelancer.UserId.ToString()),
                SalaryScore = await CalculateSalaryMatchScoreAsync(jobId, freelancer.UserId.ToString()),
                LocationScore = await CalculateLocationScoreAsync(jobId, freelancer.UserId.ToString()),
                AIReasoning = await GenerateJobMatchReasoningAsync(jobId, freelancer.UserId.ToString(), score)
            });
        }

        return results
            .OrderByDescending(x => x.OverallScore)
            .Take(count)
            .ToList();
    }

    public async Task<decimal> CalculateProjectMatchScoreAsync(Guid projectId, string freelancerId)
    {
        var skillScore = await CalculateSkillScoreAsync(projectId, freelancerId);
        var experienceScore = await CalculateExperienceScoreAsync(freelancerId);
        var budgetScore = await CalculateBudgetScoreAsync(projectId, freelancerId);
        var availabilityScore = await CalculateAvailabilityScoreAsync(freelancerId);
        var reputationScore = await CalculateReputationScoreAsync(freelancerId);

        var weights = await GetScoringWeightsAsync();

        return (skillScore * weights.Skill) +
               (experienceScore * weights.Experience) +
               (budgetScore * weights.Budget) +
               (availabilityScore * weights.Availability) +
               (reputationScore * weights.Reputation);
    }

    public async Task<decimal> CalculateJobMatchScoreAsync(Guid jobId, string freelancerId)
    {
        var skillScore = await CalculateSkillScoreForJobAsync(jobId, freelancerId);
        var experienceScore = await CalculateExperienceScoreAsync(freelancerId);
        var salaryScore = await CalculateSalaryMatchScoreAsync(jobId, freelancerId);
        var locationScore = await CalculateLocationScoreAsync(jobId, freelancerId);
        var reputationScore = await CalculateReputationScoreAsync(freelancerId);

        var weights = await GetScoringWeightsAsync();

        return (skillScore * weights.Skill) +
               (experienceScore * weights.Experience) +
               (salaryScore * 0.15m) +
               (locationScore * 0.10m) +
               (reputationScore * weights.Reputation);
    }

    private async Task<decimal> CalculateSkillScoreAsync(Guid projectId, string freelancerId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null) return 0;

        var projectSkills = project.Skills?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim().ToLower())
            .ToList() ?? new List<string>();

        if (!projectSkills.Any()) return 1.0m;

        var freelancerSkills = await _freelancerSkillRepository.GetByFreelancerIdAsync(freelancerId);
        var freelancerSkillNames = freelancerSkills
            .Select(fs => fs.Skill?.Name?.ToLower() ?? "")
            .ToList();

        var matchingSkills = projectSkills.Count(ps =>
            freelancerSkillNames.Any(fs => fs.Contains(ps) || ps.Contains(fs)));

        return matchingSkills > 0 ? (decimal)matchingSkills / projectSkills.Count : 0;
    }

    private async Task<decimal> CalculateSkillScoreForJobAsync(Guid jobId, string freelancerId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        if (job == null) return 0;

        var jobSkills = job.SkillsRequired?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim().ToLower())
            .ToList() ?? new List<string>();

        if (!jobSkills.Any()) return 1.0m;

        var freelancerSkills = await _freelancerSkillRepository.GetByFreelancerIdAsync(freelancerId);
        var freelancerSkillNames = freelancerSkills
            .Select(fs => fs.Skill?.Name?.ToLower() ?? "")
            .ToList();

        var matchingSkills = jobSkills.Count(js =>
            freelancerSkillNames.Any(fs => fs.Contains(js) || js.Contains(fs)));

        return matchingSkills > 0 ? (decimal)matchingSkills / jobSkills.Count : 0;
    }

    private async Task<decimal> CalculateExperienceScoreAsync(string freelancerId)
    {
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return 0.5m;

        var completedProjects = profile.CompletedProjects;
        return completedProjects switch
        {
            0 => 0.2m,
            < 5 => 0.4m,
            < 10 => 0.6m,
            < 20 => 0.8m,
            _ => 1.0m
        };
    }

    private async Task<decimal> CalculateBudgetScoreAsync(Guid projectId, string freelancerId)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null) return 0.5m;

        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return 0.5m;

        var avgRate = profile.HourlyRate;
        var projectBudget = (project.BudgetMax + project.BudgetMin) / 2;
        var expectedBudget = avgRate * 40;

        return expectedBudget <= projectBudget ? 1.0m : 0.5m;
    }

    private async Task<decimal> CalculateSalaryMatchScoreAsync(Guid jobId, string freelancerId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        if (job == null) return 0.5m;

        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return 0.5m;

        var expectedSalary = profile.HourlyRate;
        var jobSalaryMin = job.BudgetMin;
        var jobSalaryMax = job.BudgetMax;

        return expectedSalary >= jobSalaryMin && expectedSalary <= jobSalaryMax ? 1.0m : 0.5m;
    }

    private async Task<decimal> CalculateLocationScoreAsync(Guid jobId, string freelancerId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        if (job == null) return 0.5m;

        if (job.IsRemote) return 1.0m;

        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return 0.5m;

        var jobLocation = job.Location?.ToLower() ?? "";
        var profileLocation = profile.CountryName?.ToLower() ?? "";

        return string.IsNullOrEmpty(jobLocation) || string.IsNullOrEmpty(profileLocation)
            ? 0.5m
            : jobLocation.Contains(profileLocation) || profileLocation.Contains(jobLocation) ? 1.0m : 0.3m;
    }

    private async Task<decimal> CalculateAvailabilityScoreAsync(string freelancerId)
    {
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        if (profile == null) return 0.5m;

        return profile.IsAvailable ? 1.0m : 0.3m;
    }

    private async Task<decimal> CalculateReputationScoreAsync(string freelancerId)
    {
        if (!Guid.TryParse(freelancerId, out var freelancerGuid)) return 0.5m;
        var reviews = await _reviewRepository.GetByRevieweeIdAsync(freelancerGuid);
        if (!reviews.Any()) return 0.5m;

        var avgRating = reviews.Average(r => r.Rating);
        return (decimal)(avgRating / 5.0);
    }

    private async Task<(decimal Skill, decimal Experience, decimal Budget, decimal Availability, decimal Reputation)> GetScoringWeightsAsync()
    {
        var rules = await _scoringRuleRepository.GetActiveRulesAsync();
        
        return (
            Skill: rules.FirstOrDefault(r => r.Code == "SKILL_WEIGHT")?.Weight ?? 0.35m,
            Experience: rules.FirstOrDefault(r => r.Code == "EXPERIENCE_WEIGHT")?.Weight ?? 0.25m,
            Budget: rules.FirstOrDefault(r => r.Code == "BUDGET_WEIGHT")?.Weight ?? 0.15m,
            Availability: rules.FirstOrDefault(r => r.Code == "AVAILABILITY_WEIGHT")?.Weight ?? 0.10m,
            Reputation: rules.FirstOrDefault(r => r.Code == "REPUTATION_WEIGHT")?.Weight ?? 0.15m
        );
    }

    private async Task<string> GenerateProjectMatchReasoningAsync(Guid projectId, string freelancerId, decimal score)
    {
        var project = await _projectRepository.GetByIdAsync(projectId);
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        var freelancerSkills = await _freelancerSkillRepository.GetByFreelancerIdAsync(freelancerId);
        var reviews = Guid.TryParse(freelancerId, out var freelancerGuid) 
            ? await _reviewRepository.GetByRevieweeIdAsync(freelancerGuid) 
            : new List<DEPI.Domain.Entities.Reviews.Review>();

        var reasoning = $"Freelancer matches project with {score:P0} compatibility. ";
        
        if (profile != null)
        {
            reasoning += $"Profile completion: {profile.ProfileCompletion}%. ";
            reasoning += $"Completed projects: {profile.CompletedProjects}. ";
        }

        if (freelancerSkills.Any())
        {
            reasoning += $"Skills matched: {string.Join(", ", freelancerSkills.Take(3).Select(s => s.Skill?.Name))}. ";
        }

        if (reviews.Any())
        {
            var avgRating = reviews.Average(r => r.Rating);
            reasoning += $"Average rating: {avgRating:F1}/5. ";
        }

        return reasoning;
    }

    private async Task<string> GenerateJobMatchReasoningAsync(Guid jobId, string freelancerId, decimal score)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);
        var profile = await _freelancerProfileRepository.GetByUserIdAsync(freelancerId);
        var freelancerSkills = await _freelancerSkillRepository.GetByFreelancerIdAsync(freelancerId);

        var reasoning = $"Freelancer matches job with {score:P0} compatibility. ";
        
        if (job != null)
        {
            reasoning += $"Job: {job.Title}. ";
            reasoning += $"Type: {job.Type}. ";
        }

        if (profile != null)
        {
            reasoning += $"Experience level: {profile.ExperienceLevel}. ";
        }

        if (freelancerSkills.Any())
        {
            reasoning += $"Key skills: {string.Join(", ", freelancerSkills.Take(3).Select(s => s.Skill?.Name))}. ";
        }

        return reasoning;
    }
}