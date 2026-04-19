namespace DEPI.Domain.Services.AI;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;

public class ProfileScore : AuditableEntity
{
    public Guid UserId { get;set; }
    public decimal SkillsScore { get;set; }
    public decimal ExperienceScore { get;set; }
    public decimal ReviewsScore { get;set; }
    public decimal ProfileCompletenessScore { get;set; }
    public decimal TotalScore { get;set; }
    public int SkillsCount { get;set; }
    public int CompletedProjectsCount { get;set; }
    public int TotalReviewsCount { get;set; }
    public decimal AverageRating { get;set; }
    public decimal ProfileCompletenessPercentage { get;set; }
    public DateTime? LastCalculatedAt { get;set; }
    public string ScoreBreakdown { get;set; } = "{}";

    public User? User { get;set; }

    private ProfileScore() { }

    public static ProfileScore Create(Guid userId)
    {
        return new ProfileScore
        {
            UserId = userId,
            SkillsScore = 0,
            ExperienceScore = 0,
            ReviewsScore = 0,
            ProfileCompletenessScore = 0,
            TotalScore = 0,
            SkillsCount = 0,
            CompletedProjectsCount = 0,
            TotalReviewsCount = 0,
            AverageRating = 0,
            ProfileCompletenessPercentage = 0,
            LastCalculatedAt = DateTime.UtcNow
        };
    }

    public void UpdateScores(
        decimal skillsScore,
        decimal experienceScore,
        decimal reviewsScore,
        decimal profileCompletenessScore,
        int skillsCount,
        int completedProjectsCount,
        int totalReviewsCount,
        decimal averageRating,
        decimal profileCompletenessPercentage)
    {
        SkillsScore = skillsScore;
        ExperienceScore = experienceScore;
        ReviewsScore = reviewsScore;
        ProfileCompletenessScore = profileCompletenessScore;
        SkillsCount = skillsCount;
        CompletedProjectsCount = completedProjectsCount;
        TotalReviewsCount = totalReviewsCount;
        AverageRating = averageRating;
        ProfileCompletenessPercentage = profileCompletenessPercentage;
        
        TotalScore = (SkillsScore * 0.40m) + 
                     (ExperienceScore * 0.20m) + 
                     (ReviewsScore * 0.20m) + 
                     (ProfileCompletenessScore * 0.20m);

        var breakdown = new
        {
            skills = new { score = SkillsScore, weight = 0.40m, contribution = SkillsScore * 0.40m },
            experience = new { score = ExperienceScore, weight = 0.20m, contribution = ExperienceScore * 0.20m },
            reviews = new { score = ReviewsScore, weight = 0.20m, contribution = ReviewsScore * 0.20m },
            profileCompleteness = new { score = ProfileCompletenessScore, weight = 0.20m, contribution = ProfileCompletenessScore * 0.20m },
            totalScore = TotalScore
        };

        ScoreBreakdown = System.Text.Json.JsonSerializer.Serialize(breakdown);
        LastCalculatedAt = DateTime.UtcNow;
    }
}
