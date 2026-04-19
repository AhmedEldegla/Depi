namespace DEPI.Domain.Services.AI;

using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Projects;

public class PricePrediction : BaseEntity
{
    public Guid ProjectId { get;set; }
    public decimal MinPredictedPrice { get;set; }
    public decimal MaxPredictedPrice { get;set; }
    public decimal AveragePredictedPrice { get;set; }
    public decimal RecommendedPrice { get;set; }
    public decimal ComplexityScore { get;set; }
    public decimal MarketDemandScore { get;set; }
    public decimal CompetitionScore { get;set; }
    public decimal ConfidenceScore { get;set; }
    public string? PriceFactors { get;set; }
    public DateTime PredictedAt { get;set; }
    public bool IsValidated { get;set; }
    public decimal? ActualPrice { get;set; }
    public DateTime? ValidatedAt { get;set; }
    public Project? Project { get;set; }

    private PricePrediction() { }

    public static PricePrediction Create(
        Guid projectId,
        decimal minPrice,
        decimal maxPrice,
        decimal avgPrice,
        decimal recommendedPrice,
        decimal complexityScore,
        decimal marketDemandScore,
        decimal competitionScore)
    {
        var confidenceScore = CalculateConfidenceScore(complexityScore, marketDemandScore, competitionScore);

        var factors = new
        {
            complexity = complexityScore,
            marketDemand = marketDemandScore,
            competition = competitionScore,
            skillLevel = "Based on project requirements",
            estimatedDuration = "Based on project scope",
            marketRates = "Based on similar projects"
        };

        return new PricePrediction
        {
            ProjectId = projectId,
            MinPredictedPrice = minPrice,
            MaxPredictedPrice = maxPrice,
            AveragePredictedPrice = avgPrice,
            RecommendedPrice = recommendedPrice,
            ComplexityScore = complexityScore,
            MarketDemandScore = marketDemandScore,
            CompetitionScore = competitionScore,
            ConfidenceScore = confidenceScore,
            PriceFactors = System.Text.Json.JsonSerializer.Serialize(factors),
            PredictedAt = DateTime.UtcNow
        };
    }

    public void Validate(decimal actualPrice)
    {
        ActualPrice = actualPrice;
        ValidatedAt = DateTime.UtcNow;
        IsValidated = true;
    }

    private static decimal CalculateConfidenceScore(decimal complexity, decimal marketDemand, decimal competition)
    {
        return Math.Min(1.0m, (complexity + marketDemand + (1 - competition)) / 3);
    }

    public static decimal CalculateMinPrice(decimal basePrice, decimal complexity)
    {
        return basePrice * (0.8m - (complexity * 0.1m));
    }

    public static decimal CalculateMaxPrice(decimal basePrice, decimal complexity)
    {
        return basePrice * (1.2m + (complexity * 0.1m));
    }
}
