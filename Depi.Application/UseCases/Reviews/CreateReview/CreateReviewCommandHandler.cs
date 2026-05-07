// Reviews/CreateReview/CreateReviewCommandHandler.cs
using AutoMapper;
using DEPI.Application.Common;
using DEPI.Application.DTOs.Reviews;
using DEPI.Application.Interfaces;
using MediatR;
using DEPI.Domain.Entities.Reviews;
namespace DEPI.Application.UseCases.Reviews.CreateReview;
public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewResponse>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper) { _reviewRepository = reviewRepository; _mapper = mapper; }
    public async Task<ReviewResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = Review.Create(request.ReviewerId, request.RevieweeId, request.Rating, request.Comment, request.Type, request.ProjectId, request.ContractId);
        await _reviewRepository.AddAsync(review, cancellationToken);
        return _mapper.Map<ReviewResponse>(review);
    }
}