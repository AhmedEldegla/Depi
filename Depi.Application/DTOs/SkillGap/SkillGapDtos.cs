namespace DEPI.Application.UseCases.DTOs.SkillGap;

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