using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DEPI.Application.DTOs.Messaging;
using Microsoft.AspNetCore.Authorization;
using DEPI.Application.UseCases.Messaging;
using Depi.Application.UseCases.Messaging;
using DEPI.Application.UseCases.Identity.GetCurrentUser;
using MediatR;


namespace Depi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NotificationsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetNotifications([FromQuery] bool unreadOnly = false, CancellationToken ct = default)
        {
            var query = new GetNotificationsQuery(GetCurrentUserId(), unreadOnly);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }


        [HttpPut("{id:guid}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken ct)
        {
            try
            {
                var command = new MarkNotificationReadCommand(id, GetCurrentUserId());
                await _mediator.Send(command, ct);
                return Ok(new { message = "Notification marked as read" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }


        [HttpPut("{id:guid}/unread")]
        public async Task<IActionResult> MarkAsUnread(Guid id, CancellationToken ct)
        {
            try
            {
                var command = new MarkNotificationUnreadCommand(id, GetCurrentUserId());
                await _mediator.Send(command, ct);
                return Ok(new { message = "Notification marked as unread" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }








        private Guid GetCurrentUserId()
        {
            var sub = User.FindFirst("sub")?.Value
                ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
        }
    }


}

