using MediatR;

namespace DEPI.Application.DTOs.Pricing;

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

public record PredictPriceRequest(
    string Skills,
    string ExperienceLevel,
    string? Description,
    string? Category,
    decimal? BudgetMin,
    decimal? BudgetMax
);

public record PredictPriceCommand(
    PredictPriceRequest Request
) : IRequest<PricePredictionResponse>;