using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DEPI.Application.UseCases.Proposals.SubmitProposal;
using DEPI.Application.UseCases.Proposals.AcceptProposal;
using DEPI.Application.UseCases.Proposals.RejectProposal;
using DEPI.Application.UseCases.Proposals.WithdrawProposal;
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