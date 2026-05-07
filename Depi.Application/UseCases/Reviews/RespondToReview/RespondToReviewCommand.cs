using DEPI.Application.DTOs.Reviews;
using MediatR;
namespace DEPI.Application.UseCases.Reviews.RespondToReview;
public record RespondToReviewCommand(Guid UserId, Guid ReviewId, string Response) : IRequest<ReviewResponse>;