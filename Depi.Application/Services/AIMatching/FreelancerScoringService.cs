using DEPI.Application.Interfaces;
using DEPI.Application.Repositories.AIMatching;
using DEPI.Application.Repositories.Profiles;
using DEPI.Domain.Entities.AIMatching;
using DEPI.Domain.Entities.Profiles;
using DEPI.Domain.Entities.Reviews;

namespace DEPI.Application.Services.AIMatching;

public class FreelancerScoringService : IFreelancerScoringService
{
    private readonly IFreelancerScoreRepository _freelancerScoreRepository;
    private readonly IFreelancerProfileRepository _profileRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IScoringRuleRepository _scoringRuleRepository;

    public FreelancerScoringService(
        IFreelancerScoreRepository freelancerScoreRepository,
        IFreelancerProfileRepository profileRepository,
        IReviewRepository reviewRepository,
        IScoringRuleRepository scoringRuleRepository)
    {
        _freelancerScoreRepository = freelancerScoreRepository;
        _profileRepository = profileRepository;
        _reviewRepository = reviewRepository;
        _scoringRuleRepository = scoringRuleRepository;
    }

    public async Task<FreelancerScoreResult> CalculateFreelancerScoreAsync(Guid freelancerId)
    {
        var profile = await _profileRepository.GetByUserIdAsync(freelancerId);
        var reviews = await _reviewRepository.GetByRevieweeIdAsync(freelancerId);

        var skillScore = CalculateSkillScore(profile);
        var projectSuccessScore = CalculateProjectSuccessScore(profile);
        var communicationScore = CalculateCommunicationScore(reviews);
        var reliabilityScore = CalculateReliabilityScore(reviews);
        var responsivenessScore = CalculateResponsivenessScore(profile);
        var qualityScore = CalculateQualityScore(reviews);
        var earningsScore = CalculateEarningsScore(profile);
        var clientSatisfactionScore = CalculateClientSatisfactionScore(reviews);
        var completionRateScore = CalculateCompletionRateScore(profile);

        var weights = await GetScoringWeightsAsync();

        var overallScore = 
            (skillScore * weights.SkillScore) +
            (projectSuccessScore * weights.ProjectSuccessScore) +
            (communicationScore * weights.CommunicationScore) +
            (reliabilityScore * weights.ReliabilityScore) +
            (responsivenessScore * weights.ResponsivenessScore) +
            (qualityScore * weights.QualityScore) +
            (earningsScore * weights.EarningsScore) +
            (clientSatisfactionScore * weights.ClientSatisfactionScore) +
            (completionRateScore * weights.CompletionRateScore);

        return new FreelancerScoreResult
        {
            FreelancerId = freelancerId,
            OverallScore = Math.Round(overallScore, 2),
            SkillScore = skillScore,
            ProjectSuccessScore = projectSuccessScore,
            CommunicationScore = communicationScore,
            ReliabilityScore = reliabilityScore,
            ResponsivenessScore = responsivenessScore,
            QualityScore = qualityScore,
            EarningsScore = earningsScore,
            ClientSatisfactionScore = clientSatisfactionScore,
            CompletionRateScore = completionRateScore,
            TotalProjects = profile?.CompletedProjects ?? 0,
            CompletedProjects = profile?.CompletedProjects ?? 0,
            AverageRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0,
            TotalEarnings = profile?.TotalEarnings ?? 0,
            ResponseRate = profile?.ResponseRate ?? 0,
            OnTimeDeliveryRate = profile?.OnTimeDeliveryRate ?? 0
        };
    }

    public async Task<FreelancerScoreResult> GetOrCalculateScoreAsync(Guid freelancerId)
    {
        var existingScore = await _freelancerScoreRepository.GetByFreelancerIdAsync(freelancerId);
        
        if (existingScore != null && existingScore.CalculatedAt > DateTime.UtcNow.AddDays(-1))
        {
            return MapToResult(existingScore);
        }

        var calculatedScore = await CalculateFreelancerScoreAsync(freelancerId);
        
        await SaveFreelancerScoreAsync(calculatedScore);
        
        return calculatedScore;
    }

    public async Task<List<FreelancerScoreResult>> GetTopScoredFreelancersAsync(int count = 100)
    {
        var scores = await _freelancerScoreRepository.GetTopScoredAsync(count);
        return scores.Select(MapToResult).ToList();
    }

    public async Task<Dictionary<string, decimal>> GetScoreBreakdownAsync(Guid freelancerId)
    {
        var score = await CalculateFreelancerScoreAsync(freelancerId);
        
        return new Dictionary<string, decimal>
        {
            ["SkillScore"] = score.SkillScore,
            ["ProjectSuccessScore"] = score.ProjectSuccessScore,
            ["CommunicationScore"] = score.CommunicationScore,
            ["ReliabilityScore"] = score.ReliabilityScore,
            ["ResponsivenessScore"] = score.ResponsivenessScore,
            ["QualityScore"] = score.QualityScore,
            ["EarningsScore"] = score.EarningsScore,
            ["ClientSatisfactionScore"] = score.ClientSatisfactionScore,
            ["CompletionRateScore"] = score.CompletionRateScore,
            ["OverallScore"] = score.OverallScore
        };
    }

    private decimal CalculateSkillScore(UserProfile? profile)
    {
        if (profile == null) return 0.5m;

        var skillsCount = profile.SkillsCount;
        
        return skillsCount switch
        {
            0 => 0.1m,
            < 5 => 0.3m,
            < 10 => 0.5m,
            < 20 => 0.7m,
            < 30 => 0.85m,
            _ => 1.0m
        };
    }

    private decimal CalculateProjectSuccessScore(UserProfile? profile)
    {
        if (profile == null) return 0.5m;

        var totalProjects = profile.CompletedProjects;
        var successRate = profile.CompletionRate;

        if (totalProjects == 0) return 0.3m;

        return successRate switch
        {
            < 0.5m => 0.2m,
            < 0.7m => 0.4m,
            < 0.85m => 0.6m,
            < 0.95m => 0.8m,
            _ => 1.0m
        };
    }

    private decimal CalculateCommunicationScore(IEnumerable<Review> reviews)
    {
        if (!reviews.Any()) return 0.5m;

        var communicationRatings = reviews
            .Where(r => r.Rating >= 4)
            .Sum(r => 1);

        var ratio = (decimal)communicationRatings / reviews.Count();
        
        return ratio switch
        {
            < 0.5m => 0.3m,
            < 0.7m => 0.5m,
            < 0.85m => 0.7m,
            < 0.95m => 0.9m,
            _ => 1.0m
        };
    }

    private decimal CalculateReliabilityScore(IEnumerable<Review> reviews)
    {
        if (!reviews.Any()) return 0.5m;

        var positiveReviews = reviews
            .Where(r => r.Rating >= 4)
            .Count();

        var ratio = (decimal)positiveReviews / reviews.Count();
        
        return ratio switch
        {
            < 0.5m => 0.3m,
            < 0.7m => 0.5m,
            < 0.85m => 0.7m,
            < 0.95m => 0.9m,
            _ => 1.0m
        };
    }

    private decimal CalculateResponsivenessScore(UserProfile? profile)
    {
        if (profile == null) return 0.5m;

        var responseRate = profile.ResponseRate;
        
        return responseRate switch
        {
            < 0.5m => 0.2m,
            < 0.7m => 0.4m,
            < 0.85m => 0.6m,
            < 0.95m => 0.8m,
            _ => 1.0m
        };
    }

    private decimal CalculateQualityScore(IEnumerable<Review> reviews)
    {
        if (!reviews.Any()) return 0.5m;

        var avgRating = (decimal)reviews.Average(r => r.Rating);
        
        return avgRating switch
        {
            < 3m => 0.2m,
            < 3.5m => 0.4m,
            < 4m => 0.6m,
            < 4.5m => 0.8m,
            _ => 1.0m
        };
    }

    private decimal CalculateEarningsScore(UserProfile? profile)
    {
        if (profile == null) return 0.3m;

        var earnings = profile.TotalEarnings;
        
        return earnings switch
        {
            < 1000m => 0.2m,
            < 5000m => 0.4m,
            < 15000m => 0.6m,
            < 50000m => 0.8m,
            _ => 1.0m
        };
    }

    private decimal CalculateClientSatisfactionScore(IEnumerable<Review> reviews)
    {
        if (!reviews.Any()) return 0.5m;

        var avgRating = (decimal)reviews.Average(r => r.Rating);
        return Math.Min(avgRating / 5m, 1m);
    }

    private decimal CalculateCompletionRateScore(UserProfile? profile)
    {
        if (profile == null) return 0.5m;

        var completionRate = profile.CompletionRate;
        
        return completionRate switch
        {
            < 0.5m => 0.2m,
            < 0.7m => 0.4m,
            < 0.85m => 0.6m,
            < 0.95m => 0.8m,
            _ => 1.0m
        };
    }

    private async Task<(decimal SkillScore, decimal ProjectSuccessScore, decimal CommunicationScore, 
        decimal ReliabilityScore, decimal ResponsivenessScore, decimal QualityScore, 
        decimal EarningsScore, decimal ClientSatisfactionScore, decimal CompletionRateScore)> GetScoringWeightsAsync()
    {
        var rules = await _scoringRuleRepository.GetActiveRulesAsync();
        
        return (
            SkillScore: rules.FirstOrDefault(r => r.Code == "SKILL_SCORE")?.Weight ?? 0.15m,
            ProjectSuccessScore: rules.FirstOrDefault(r => r.Code == "PROJECT_SUCCESS")?.Weight ?? 0.15m,
            CommunicationScore: rules.FirstOrDefault(r => r.Code == "COMMUNICATION")?.Weight ?? 0.10m,
            ReliabilityScore: rules.FirstOrDefault(r => r.Code == "RELIABILITY")?.Weight ?? 0.10m,
            ResponsivenessScore: rules.FirstOrDefault(r => r.Code == "RESPONSIVENESS")?.Weight ?? 0.08m,
            QualityScore: rules.FirstOrDefault(r => r.Code == "QUALITY")?.Weight ?? 0.15m,
            EarningsScore: rules.FirstOrDefault(r => r.Code == "EARNINGS")?.Weight ?? 0.10m,
            ClientSatisfactionScore: rules.FirstOrDefault(r => r.Code == "CLIENT_SATISFACTION")?.Weight ?? 0.12m,
            CompletionRateScore: rules.FirstOrDefault(r => r.Code == "COMPLETION_RATE")?.Weight ?? 0.05m
        );
    }

    private FreelancerScoreResult MapToResult(FreelancerScore score)
    {
        return new FreelancerScoreResult
        {
            FreelancerId = score.FreelancerId,
            OverallScore = score.OverallScore,
            SkillScore = score.SkillScore,
            ProjectSuccessScore = score.ProjectSuccessScore,
            CommunicationScore = score.CommunicationScore,
            ReliabilityScore = score.ReliabilityScore,
            ResponsivenessScore = score.ResponsivenessScore,
            QualityScore = score.QualityScore,
            EarningsScore = score.EarningsScore,
            ClientSatisfactionScore = score.ClientSatisfactionScore,
            CompletionRateScore = score.CompletionRateScore,
            TotalProjects = score.TotalProjects,
            CompletedProjects = score.CompletedProjects,
            AverageRating = score.AverageRating,
            TotalEarnings = score.TotalEarnings,
            ResponseRate = score.ResponseRate,
            OnTimeDeliveryRate = score.OnTimeDeliveryRate
        };
    }

    private async Task SaveFreelancerScoreAsync(FreelancerScoreResult result)
    {
        var score = new FreelancerScore
        {
            FreelancerId = result.FreelancerId,
            OverallScore = result.OverallScore,
            SkillScore = result.SkillScore,
            ProjectSuccessScore = result.ProjectSuccessScore,
            CommunicationScore = result.CommunicationScore,
            ReliabilityScore = result.ReliabilityScore,
            ResponsivenessScore = result.ResponsivenessScore,
            QualityScore = result.QualityScore,
            EarningsScore = result.EarningsScore,
            ClientSatisfactionScore = result.ClientSatisfactionScore,
            CompletionRateScore = result.CompletionRateScore,
            TotalProjects = result.TotalProjects,
            CompletedProjects = result.CompletedProjects,
            AverageRating = result.AverageRating,
            TotalEarnings = result.TotalEarnings,
            ResponseRate = result.ResponseRate,
            OnTimeDeliveryRate = result.OnTimeDeliveryRate,
            CalculatedAt = DateTime.UtcNow
        };

        var existing = await _freelancerScoreRepository.GetByFreelancerIdAsync(result.FreelancerId);
        if (existing != null)
        {
            existing.UpdateScore(result.OverallScore, result.SkillScore, result.ProjectSuccessScore,
                result.CommunicationScore, result.ReliabilityScore, result.ResponsivenessScore,
                result.QualityScore, result.EarningsScore, result.ClientSatisfactionScore,
                result.CompletionRateScore);
            await _freelancerScoreRepository.UpdateAsync(existing);
        }
        else
        {
            await _freelancerScoreRepository.AddAsync(score);
        }
    }
}