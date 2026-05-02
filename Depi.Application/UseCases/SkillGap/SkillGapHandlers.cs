using DEPI.Application.DTOs.Profiles;
using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.Learning;
using DEPI.Application.Repositories.Recruitment;
using DEPI.Domain.Entities.Learning;
using MediatR;

namespace DEPI.Application.UseCases.SkillGap;

public class SkillGapResponse
{
    public string SkillName { get; set; } = string.Empty;
    public int MarketDemand { get; set; }
    public string DemandLevel { get; set; } = string.Empty;
    public decimal AverageBudget { get; set; }
    public int ActiveProjects { get; set; }
    public int ActiveJobs { get; set; }
    public List<CourseRecommendation> RecommendedCourses { get; set; } = new();
}

public class CourseRecommendation
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public string Url { get; set; } = string.Empty;
}

public class SkillGapSummaryResponse
{
    public string FreelancerName { get; set; } = string.Empty;
    public int TotalActiveMarketOpportunities { get; set; }
    public List<string> CurrentSkills { get; set; } = new();
    public List<SkillGapResponse> Gaps { get; set; } = new();
    public int GapsFound { get; set; }
    public string Summary { get; set; } = string.Empty;
}

public record GetSkillGapAnalysisQuery(Guid UserId) : IRequest<SkillGapSummaryResponse>;

public class GetSkillGapAnalysisQueryHandler : IRequestHandler<GetSkillGapAnalysisQuery, SkillGapSummaryResponse>
{
    private readonly IProjectRepository _projectRepo;
    private readonly IJobRepository _jobRepo;
    private readonly ICourseRepository _courseRepo;
    private readonly ILearningPathRepository _pathRepo;

    public GetSkillGapAnalysisQueryHandler(IProjectRepository projectRepo, IJobRepository jobRepo, ICourseRepository courseRepo, ILearningPathRepository pathRepo)
    { _projectRepo = projectRepo; _jobRepo = jobRepo; _courseRepo = courseRepo; _pathRepo = pathRepo; }

    public async Task<SkillGapSummaryResponse> Handle(GetSkillGapAnalysisQuery r, CancellationToken ct)
    {
        var allProjects = await _projectRepo.GetAllProjectsAsync(ct);
        var openProjects = allProjects.Where(p => p.Skills != null && p.Status == Domain.Enums.ProjectStatus.Open).ToList();
        var activeJobs = await _jobRepo.GetActiveJobsAsync();

        var marketSkills = new Dictionary<string, MarketSkillData>();
        var totalOpportunities = 0;

        foreach (var project in openProjects)
        {
            var skills = (project.Skills ?? "").Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s));
            var avgBudget = (project.BudgetMin + project.BudgetMax) / 2 ?? project.FixedPrice ?? 0;
            foreach (var skill in skills)
            {
                if (!marketSkills.ContainsKey(skill))
                    marketSkills[skill] = new MarketSkillData();
                marketSkills[skill].ProjectCount++;
                marketSkills[skill].TotalBudget += avgBudget;
                totalOpportunities++;
            }
        }

        foreach (var job in activeJobs)
        {
            var skills = (job.SkillsRequired ?? "").Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s));
            var avgBudget = (job.BudgetMin + job.BudgetMax) / 2;
            foreach (var skill in skills)
            {
                if (!marketSkills.ContainsKey(skill))
                    marketSkills[skill] = new MarketSkillData();
                marketSkills[skill].JobCount++;
                marketSkills[skill].TotalBudget += avgBudget;
                totalOpportunities++;
            }
        }

        var allCourses = (await _courseRepo.GetAllAsync(ct)).ToList();
        var topGaps = marketSkills.OrderByDescending(kv => kv.Value.ProjectCount + kv.Value.JobCount).Take(10).ToList();

        var gaps = topGaps.Select(kv =>
        {
            var data = kv.Value;
            var total = data.ProjectCount + data.JobCount;
            var demandLevel = total >= 10 ? "Very High" : total >= 5 ? "High" : total >= 2 ? "Medium" : "Low";
            var avgBudget = data.ProjectCount + data.JobCount > 0 ? data.TotalBudget / (data.ProjectCount + data.JobCount) : 0;

            var matchingCourses = allCourses
                .Where(c => (c.Title.Contains(kv.Key, StringComparison.OrdinalIgnoreCase) || c.Category.Contains(kv.Key, StringComparison.OrdinalIgnoreCase) || c.Tags.Contains(kv.Key, StringComparison.OrdinalIgnoreCase)) && c.IsPublished)
                .Take(3)
                .Select(c => new CourseRecommendation { CourseId = c.Id, Title = c.Title, Level = c.Level.ToString(), Price = c.Price, IsFree = c.IsFree, Url = $"/courses/{c.Id}" })
                .ToList();

            return new SkillGapResponse
            {
                SkillName = kv.Key,
                MarketDemand = total,
                DemandLevel = demandLevel,
                AverageBudget = Math.Round(avgBudget, 2),
                ActiveProjects = data.ProjectCount,
                ActiveJobs = data.JobCount,
                RecommendedCourses = matchingCourses
            };
        }).ToList();

        return new SkillGapSummaryResponse
        {
            FreelancerName = "",
            TotalActiveMarketOpportunities = totalOpportunities,
            CurrentSkills = new List<string>(),
            Gaps = gaps,
            GapsFound = gaps.Count,
            Summary = gaps.Count > 0
                ? $"Market analysis found {gaps.Count} in-demand skills. Top skill: {gaps.First().SkillName} with {gaps.First().MarketDemand} active opportunities."
                : "No market gaps detected. Keep up the great work!"
        };
    }

    private class MarketSkillData
    {
        public int ProjectCount { get; set; }
        public int JobCount { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
