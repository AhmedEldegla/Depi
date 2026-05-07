namespace DEPI.Application.DTOs.SkillGap;

public class CourseRecommendation
{
    public Guid CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public string Url { get; set; } = string.Empty;
}