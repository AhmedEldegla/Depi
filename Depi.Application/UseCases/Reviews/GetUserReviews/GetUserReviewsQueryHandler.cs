using AutoMapper;
using DEPI.Application.DTOs.Reviews;
using DEPI.Application.Interfaces;
using MediatR;

namespace DEPI.Application.UseCases.Reviews.GetUserReviews;

public class GetUserReviewsQueryHandler : IRequestHandler<GetUserReviewsQuery, ReviewListResponse>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;

    public GetUserReviewsQueryHandler(IReviewRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ReviewListResponse> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetByRevieweeIdAsync(request.UserId, cancellationToken);
        var count = reviews.Count();
        var avgRating = count > 0 ? (decimal)reviews.Average(r => r.Rating) : 0;
        var distribution = reviews.GroupBy(r => r.Rating).ToDictionary(g => g.Key, g => g.Count());

        return new ReviewListResponse
        {
            Reviews = reviews.Select(r => _mapper.Map<ReviewResponse>(r)).ToList(),
            TotalCount = count,
            AverageRating = avgRating,
            FiveStarsCount = distribution.GetValueOrDefault(5, 0),
            FourStarsCount = distribution.GetValueOrDefault(4, 0),
            ThreeStarsCount = distribution.GetValueOrDefault(3, 0),
            TwoStarsCount = distribution.GetValueOrDefault(2, 0),
            OneStarCount = distribution.GetValueOrDefault(1, 0)
        };
    }
}