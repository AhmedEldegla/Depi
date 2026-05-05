using DEPI.Application.UseCases.Reviews.CreateReview;
using DEPI.Application.UseCases.Reviews.RespondToReview;
using DEPI.Application.UseCases.Reviews.GetUserReviews;
using DEPI.Application.DTOs.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReviewsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ReviewsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin,Client,Freelancer")]
    public async Task<IActionResult> Create([FromBody] CreateReviewRequestDto request, CancellationToken ct)
        => Created("", await _mediator.Send(new CreateReviewCommand(GetCurrentUserId(), request.RevieweeId, request.Rating, request.Comment, request.Type, request.ProjectId, request.ContractId), ct));

    [HttpPost("{id:guid}/respond")]
    [Authorize(Roles = "Admin,Client,Freelancer,Coach")]
    public async Task<IActionResult> Respond(Guid id, [FromBody] RespondToReviewRequestDto request, CancellationToken ct)
        => Ok(await _mediator.Send(new RespondToReviewCommand(GetCurrentUserId(), id, request.Response), ct));

    [HttpGet("user/{userId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserReviews(Guid userId, [FromQuery] ReviewFilterRequest filter, CancellationToken ct)
        => Ok(await _mediator.Send(new GetUserReviewsQuery(userId, filter), ct));

    [HttpGet("me")]
    public async Task<IActionResult> GetMyReviews(CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var filter = new ReviewFilterRequest(userId, null, 1, 50);
        return Ok(await _mediator.Send(new GetUserReviewsQuery(userId, filter), ct));
    }

    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
    }
}
