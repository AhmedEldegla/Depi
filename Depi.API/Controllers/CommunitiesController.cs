using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEPI.Application.UseCases.Identity.GetCurrentUser;
using Microsoft.AspNetCore.Authorization;
using DEPI.Application.UseCases.Community;
using MediatR;


namespace Depi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommunitiesController(IMediator mediator) => _mediator = mediator;


        [HttpGet("posts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts( [FromQuery] bool? featured,
                                                   [FromQuery] string? category,
                                                   [FromQuery] string? searchTerm,
                                                   [FromQuery] int page = 1,
                                                   [FromQuery] int pageSize = 10,
                                                   CancellationToken ct = default)
        {
            var query = new GetPostsQuery(featured, category, searchTerm, page, pageSize);

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPost("posts")]
        [Authorize(Roles = "Admin,Client,Freelancer")]
        public async Task<IActionResult> CreatePost( [FromBody] CreatePostRequest request, CancellationToken ct)
        {
            var command = new CreatePostCommand(GetCurrentUserId(), request);

            var result = await _mediator.Send(command, ct);

            return Created($"api/community/{result.Id}", result);
        }


        [HttpGet("forum/threads")]
        [AllowAnonymous]
        public async Task<IActionResult> GetForumThreads( [FromQuery] Guid? categoryId,
                                                          [FromQuery] string? searchTerm,
                                                          [FromQuery] int page = 1,
                                                          [FromQuery] int pageSize = 10,
                                                          CancellationToken ct = default)
        {
            var query = new GetForumThreadsQuery(categoryId, searchTerm, page, pageSize);

            var result = await _mediator.Send(query, ct);

            return Ok(result);
        }

        [HttpPost("forum/threads")]
        [Authorize(Roles = "Admin,Client,Freelancer")]
        public async Task<IActionResult> CreateForumThread( [FromBody] CreateForumThreadRequest request, CancellationToken ct)
        {
            var command = new CreateForumThreadCommand(GetCurrentUserId(), request);

            var result = await _mediator.Send(command, ct);

            return Created($"api/community/forum/threads/{result.Id}", result);
        }

        [HttpPost("forum/replies")]
        [Authorize(Roles = "Admin,Client,Freelancer")]
        public async Task<IActionResult> CreateForumReply( [FromBody] CreateForumReplyRequest request, CancellationToken ct)
        {
            var command = new CreateForumReplyCommand(GetCurrentUserId(), request);

            var result = await _mediator.Send(command, ct);

            return Created($"api/community/forum/replies/{result}", result);
        }

        private Guid GetCurrentUserId()
        {
            var sub = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
        }
    }
}
