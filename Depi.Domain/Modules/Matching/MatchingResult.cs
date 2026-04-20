namespace DEPI.Domain.Services.Matching;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Projects;

public class MatchingResult : BaseEntity
{
    public Guid ProjectId { get;set; }
    public Guid FreelancerId { get;set; }
    public decimal TotalScore { get;set; }
    public decimal SkillsMatchScore { get;set; }
    public decimal ExperienceMatchScore { get;set; }
    public decimal ReviewsMatchScore { get;set; }
    public decimal ProfileMatchScore { get;set; }
    public decimal PriceMatchScore { get;set; }
    public int Rank { get;set; }
    public bool IsRecommended { get;set; }
    public string MatchReasons { get;set; } = "[]";
    public DateTime CalculatedAt { get;set; }

    public Project? Project { get;set; }
    public User? Freelancer { get;set; } 

    private MatchingResult() { }

    public static MatchingResult Create(
        Guid projectId,
        Guid freelancerId,
        decimal skillsMatchScore,
        decimal experienceMatchScore,
        decimal reviewsMatchScore,
        decimal profileMatchScore,
        decimal priceMatchScore)
    {
        var totalScore = (skillsMatchScore * 0.40m) + 
                         (experienceMatchScore * 0.20m) + 
                         (reviewsMatchScore * 0.20m) + 
                         (profileMatchScore * 0.20m);

        var reasons = new List<string>();
        if (skillsMatchScore >= 0.8m) reasons.Add("Excellent skills match");
        else if (skillsMatchScore >= 0.6m) reasons.Add("Good skills match");
        if (experienceMatchScore >= 0.8m) reasons.Add("Highly experienced");
        if (reviewsMatchScore >= 0.8m) reasons.Add("Excellent reviews");
        if (profileMatchScore >= 0.8m) reasons.Add("Complete profile");

        return new MatchingResult
        {
            ProjectId = projectId,
            FreelancerId = freelancerId,
            SkillsMatchScore = skillsMatchScore,
            ExperienceMatchScore = experienceMatchScore,
            ReviewsMatchScore = reviewsMatchScore,
            ProfileMatchScore = profileMatchScore,
            PriceMatchScore = priceMatchScore,
            TotalScore = totalScore,
            IsRecommended = totalScore >= 0.7m,
            MatchReasons = System.Text.Json.JsonSerializer.Serialize(reasons),
            CalculatedAt = DateTime.UtcNow
        };
    }

    public void SetRank(int rank)
    {
        Rank = rank;
    }

    public void UpdateScore(decimal skillsMatchScore, decimal experienceMatchScore, 
        decimal reviewsMatchScore, decimal profileMatchScore, decimal priceMatchScore)
    {
        SkillsMatchScore = skillsMatchScore;
        ExperienceMatchScore = experienceMatchScore;
        ReviewsMatchScore = reviewsMatchScore;
        ProfileMatchScore = profileMatchScore;
        PriceMatchScore = priceMatchScore;
        
        TotalScore = (skillsMatchScore * 0.40m) + 
                     (experienceMatchScore * 0.20m) + 
                     (reviewsMatchScore * 0.20m) + 
                     (profileMatchScore * 0.20m);
    }
}
