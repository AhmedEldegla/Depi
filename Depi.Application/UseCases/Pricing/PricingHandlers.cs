using MediatR;

namespace DEPI.Application.UseCases.Pricing;

public class PricePredictionResponse
{
    public decimal SuggestedMinPrice { get; set; }
    public decimal SuggestedMaxPrice { get; set; }
    public decimal RecommendedPrice { get; set; }
    public decimal MarketAverage { get; set; }
    public string Reasoning { get; set; } = string.Empty;
    public string SkillImpact { get; set; } = string.Empty;
    public string ExperienceImpact { get; set; } = string.Empty;
    public string ComplexityImpact { get; set; } = string.Empty;
    public decimal ConfidenceScore { get; set; }
}

public record PredictPriceRequest(string Skills, string ExperienceLevel, string? Description, string? Category, decimal? BudgetMin, decimal? BudgetMax);

public record PredictPriceCommand(PredictPriceRequest Request) : IRequest<PricePredictionResponse>;

public class PredictPriceCommandHandler : IRequestHandler<PredictPriceCommand, PricePredictionResponse>
{
    public Task<PricePredictionResponse> Handle(PredictPriceCommand r, CancellationToken ct)
    {
        var req = r.Request;
        var skillCount = req.Skills?.Split(',').Length ?? 1;

        var baseRate = req.ExperienceLevel?.ToLower() switch
        {
            "beginner" => 15m, "intermediate" => 35m, "expert" => 60m, _ => 25m
        };

        var skillMultiplier = 1m + (skillCount * 0.15m);
        var complexityMultiplier = (req.Description?.Length ?? 0) > 500 ? 1.4m : 1.1m;

        var suggestedMin = baseRate * skillMultiplier * complexityMultiplier;
        var suggestedMax = suggestedMin * 1.8m;
        var recommended = (suggestedMin + suggestedMax) / 2;
        var marketAvg = recommended * 0.85m;

        var isHighComplexity = (req.Description?.Length ?? 0) > 500;
        var complexityWord = isHighComplexity ? "high" : "medium";

        var response = new PricePredictionResponse
        {
            SuggestedMinPrice = Math.Round(suggestedMin, 2),
            SuggestedMaxPrice = Math.Round(suggestedMax, 2),
            RecommendedPrice = Math.Round(recommended, 2),
            MarketAverage = Math.Round(marketAvg, 2),
            ConfidenceScore = Math.Round(0.65m + (skillCount * 0.05m), 2),
            Reasoning = $"Based on {skillCount} skills at {req.ExperienceLevel} level with {complexityWord} complexity",
            SkillImpact = $"Skills multiplier: {skillMultiplier:F2}x (from {skillCount} skills)",
            ExperienceImpact = $"Base rate: ${baseRate}/hr for {req.ExperienceLevel ?? "intermediate"} level",
            ComplexityImpact = $"Complexity multiplier: {complexityMultiplier:F2}x"
        };

        return Task.FromResult(response);
    }
}
