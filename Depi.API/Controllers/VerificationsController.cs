using DEPI.Application.DTOs.Verifications;
using DEPI.Application.UseCases.Verifications.SubmitVerification;
using DEPI.Application.UseCases.Verifications.ReviewVerification;
using DEPI.Application.UseCases.Verifications.GetVerifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VerificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VerificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("submit")]
    [ProducesResponseType(typeof(IdentityVerificationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitVerification(
        [FromBody] SubmitIdentityVerificationRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId == Guid.Empty) return Unauthorized();

        try
        {
            var command = new SubmitIdentityVerificationCommand(userId, request);
            var result = await _mediator.Send(command, cancellationToken);
            return Created("", result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("my")]
    [ProducesResponseType(typeof(List<IdentityVerificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyVerifications(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        if (userId == Guid.Empty) return Unauthorized();

        var result = await _mediator.Send(new GetMyVerificationsQuery(userId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("pending")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(List<IdentityVerificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPendingVerifications(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPendingVerificationsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{id:guid}/approve")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IdentityVerificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ApproveVerification(
        Guid id, CancellationToken cancellationToken)
    {
        var adminId = GetCurrentUserId();
        if (adminId == Guid.Empty) return Unauthorized();

        try
        {
            var command = new ApproveVerificationCommand(id, adminId);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{id:guid}/reject")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IdentityVerificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RejectVerification(
        Guid id,
        [FromBody] ReviewVerificationRequest request,
        CancellationToken cancellationToken)
    {
        var adminId = GetCurrentUserId();
        if (adminId == Guid.Empty) return Unauthorized();

        try
        {
            var command = new RejectVerificationCommand(id, adminId, request.RejectionReason ?? "غير محدد");
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    private Guid GetCurrentUserId()
    {
        var subClaim = User.FindFirst("sub")?.Value
                    ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(subClaim, out var userId))
            return userId;

        return Guid.Empty;
    }
}
