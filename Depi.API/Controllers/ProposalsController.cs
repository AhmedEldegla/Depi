using DEPI.Application.DTOs.Proposals;
using DEPI.Application.UseCases.Proposals.SubmitProposal;
using DEPI.Application.UseCases.Proposals.AcceptProposal;
using DEPI.Application.UseCases.Proposals.RejectProposal;
using DEPI.Application.UseCases.Proposals.WithdrawProposal;
using DEPI.Application.UseCases.Proposals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProposalsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProposalsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin,Freelancer,Student")]
    public async Task<IActionResult> Submit([FromBody] SubmitProposalRequest request, CancellationToken ct)
    {
        var freelancerId = GetCurrentUserId();
        var result = await _mediator.Send(new SubmitProposalCommand(freelancerId, request), ct);
        return Created("", result);
    }

    [HttpPost("{id:guid}/accept")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Accept(Guid id, CancellationToken ct)
    {
        var clientId = GetCurrentUserId();
        var result = await _mediator.Send(new AcceptProposalCommand(id, clientId), ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/reject")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Reject(Guid id, [FromBody] string? reason, CancellationToken ct)
    {
        var clientId = GetCurrentUserId();
        var result = await _mediator.Send(new RejectProposalCommand(id, clientId, reason ?? "Not specified"), ct);
        return Ok(result);
    }

    [HttpPost("{id:guid}/withdraw")]
    [Authorize(Roles = "Admin,Freelancer,Student")]
    public async Task<IActionResult> Withdraw(Guid id, CancellationToken ct)
    {
        var freelancerId = GetCurrentUserId();
        await _mediator.Send(new WithdrawProposalCommand(id, freelancerId), ct);
        return NoContent();
    }

    [HttpGet("my-proposals")]
    [Authorize(Roles = "Admin,Freelancer,Student")]
    public async Task<IActionResult> MyProposals(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetMyProposalsQuery(GetCurrentUserId()), ct);
        return Ok(result);
    }

    [HttpGet("project/{projectId:guid}")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> ByProject(Guid projectId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProposalsByProjectQuery(projectId), ct);
        return Ok(result);
    }

    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
    }
}
