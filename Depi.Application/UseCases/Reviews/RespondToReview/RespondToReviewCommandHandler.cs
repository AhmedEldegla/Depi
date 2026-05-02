using AutoMapper;
using DEPI.Application.DTOs.Reviews;
using DEPI.Application.Interfaces;
using MediatR;
namespace DEPI.Application.UseCases.Reviews.RespondToReview;
public class RespondToReviewCommandHandler : IRequestHandler<RespondToReviewCommand, ReviewResponse>
{
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;
    public RespondToReviewCommandHandler(IReviewRepository repository, IMapper mapper) { _repository = repository; _mapper = mapper; }
    public async Task<ReviewResponse> Handle(RespondToReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.ReviewId, cancellationToken)
            ?? throw new KeyNotFoundException("التقييم غير موجود");
        if (review.RevieweeId != request.UserId)
            throw new UnauthorizedAccessException("ليس لديك صلاحية الرد على هذا التقييم");
        review.AddResponse(request.Response);
        await _repository.UpdateAsync(review, cancellationToken);
        return _mapper.Map<ReviewResponse>(review);
    }
}