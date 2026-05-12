using DEPI.Application.UseCases.DTOs.SkillGap;

namespace DEPI.Application.DTOs.SkillGap;

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