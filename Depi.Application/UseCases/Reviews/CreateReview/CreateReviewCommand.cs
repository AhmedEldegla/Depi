// Reviews/CreateReview/CreateReviewCommand.cs
using DEPI.Application.DTOs.Reviews;
using DEPI.Domain.Entities.Reviews;
using MediatR;
namespace DEPI.Application.UseCases.Reviews.CreateReview;
public record CreateReviewCommand(Guid ReviewerId, Guid RevieweeId, int Rating, string Comment, ReviewType Type, Guid? ProjectId, Guid? ContractId) : IRequest<ReviewResponse>;