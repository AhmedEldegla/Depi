using DEPI.Domain.Entities.Reviews;

namespace DEPI.Application.DTOs.Reviews;

public record CreateReviewRequestDto(
    Guid RevieweeId,
    int Rating,
    string Comment,
    ReviewType Type,
    Guid? ProjectId,
    Guid? ContractId
);

public record RespondToReviewRequestDto(
    string Response
);

public class ReviewResponse
{
    public Guid Id { get; set; }
    public Guid ReviewerId { get; set; }
    public Guid RevieweeId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? ContractId { get; set; }
    public ReviewType Type { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string? Response { get; set; }
    public DateTime? ResponseAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public record ReviewFilterRequest(
    Guid? RevieweeId,
    ReviewType? Type,
    int Page = 1,
    int PageSize = 20
);

public class ReviewListResponse
{
    public List<ReviewResponse> Reviews { get; set; } = new();
    public int TotalCount { get; set; }
    public decimal AverageRating { get; set; }
    public int FiveStarsCount { get; set; }
    public int FourStarsCount { get; set; }
    public int ThreeStarsCount { get; set; }
    public int TwoStarsCount { get; set; }
    public int OneStarCount { get; set; }
}