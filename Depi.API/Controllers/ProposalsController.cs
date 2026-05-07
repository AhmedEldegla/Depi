<<<<<<< HEAD
﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

=======
using DEPI.Application.DTOs.Proposals;
>>>>>>> master
using DEPI.Application.UseCases.Proposals.SubmitProposal;
using DEPI.Application.UseCases.Proposals.AcceptProposal;
using DEPI.Application.UseCases.Proposals.RejectProposal;
using DEPI.Application.UseCases.Proposals.WithdrawProposal;
<<<<<<< HEAD
using DEPI.Application.UseCases.Proposals.GetMyProposals;
using DEPI.Application.UseCases.Proposals.GetProposalsByProject;
using DEPI.Application.DTOs.Proposals;

namespace Depi.API.Controllers;

[ApiController]
[Route("api/proposals")]
public class ProposalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProposalsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Submit Proposal (Freelancer)
    [Authorize(Roles = "Freelancer")]
    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmitProposalRequest request)
    {
        var userId = GetUserId();

        var result = await _mediator.Send(
            new SubmitProposalCommand(userId, request));

        return Ok(result);
    }

    // Accept Proposal (Client)
    [Authorize(Roles = "Client")]
    [HttpPost("{id:guid}/accept")]
    public async Task<IActionResult> Accept(Guid id)
    {
        var userId = GetUserId();

        var result = await _mediator.Send(
            new AcceptProposalCommand(id, userId));

        return Ok(result);
    }

    // Reject Proposal (Client)
    [Authorize(Roles = "Client")]
    [HttpPost("{id:guid}/reject")]
    public async Task<IActionResult> Reject(Guid id, [FromBody] string? reason)
    {
        var userId = GetUserId();

        var result = await _mediator.Send(
            new RejectProposalCommand(id, userId, reason));

        return Ok(result);
    }

    // Withdraw Proposal (Freelancer)
    [Authorize(Roles = "Freelancer")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Withdraw(Guid id)
    {
        var userId = GetUserId();

        await _mediator.Send(
            new WithdrawProposalCommand(id, userId));

        return NoContent();
    }

    // My Proposals (Freelancer)
    [Authorize(Roles = "Freelancer")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        var userId = GetUserId();

        var result = await _mediator.Send(
            new GetMyProposalsQuery(userId));

        return Ok(result);
    }

    // Get Proposals by Project

    [Authorize]
    [HttpGet("project/{projectId:guid}")]
    public async Task<IActionResult> GetByProject(Guid projectId)
    {
        var result = await _mediator.Send(
            new GetProposalsByProjectQuery(projectId));

        return Ok(result);
    }

    // Helper
    private Guid GetUserId()
    {
        return Guid.Parse(
            User.FindFirst("sub")?.Value
            ?? throw new UnauthorizedAccessException("User not authenticated")
        );
    }
}
=======
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
>>>>>>> master
