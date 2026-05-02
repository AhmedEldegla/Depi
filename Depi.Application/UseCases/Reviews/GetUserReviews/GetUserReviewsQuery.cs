using DEPI.Application.DTOs.Reviews;
using DEPI.Domain.Entities.Reviews;
using MediatR;
namespace DEPI.Application.UseCases.Reviews.GetUserReviews;
public record GetUserReviewsQuery(Guid UserId, ReviewFilterRequest? Filter = null) : IRequest<ReviewListResponse>;