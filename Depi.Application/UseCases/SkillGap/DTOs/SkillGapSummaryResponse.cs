namespace DEPI.Application.DTOs.SkillGap;

public class SkillGapSummaryResponse
{
    public string FreelancerName { get; set; } = string.Empty;
    public int TotalActiveMarketOpportunities { get; set; }
    public List<string> CurrentSkills { get; set; } = new();
    public List<SkillGapResponse> Gaps { get; set; } = new();
    public int GapsFound { get; set; }
    public string Summary { get; set; } = string.Empty;
}