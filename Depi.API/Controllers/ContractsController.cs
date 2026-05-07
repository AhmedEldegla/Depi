using DEPI.Application.DTOs.Contracts;
using DEPI.Application.UseCases.Contracts.AddMilestone;
using DEPI.Application.UseCases.Contracts.CompleteContract;
using DEPI.Application.UseCases.Contracts.CompleteMilestone;
using DEPI.Application.UseCases.Contracts.CreateContract;
using DEPI.Application.UseCases.Contracts.GetContractById;
using DEPI.Application.UseCases.Contracts.GetMyContracts;
using DEPI.Application.UseCases.Contracts.PauseContract;
using DEPI.Application.UseCases.Contracts.StartContract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContractsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContractsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Create([FromBody] CreateContractRequest request, CancellationToken ct)
    {
        try { var userId = GetCurrentUserId(); var result = await _mediator.Send(new CreateContractCommand(userId, request), ct); return Created($"api/contracts/{result.Id}", result); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        try { return Ok(await _mediator.Send(new GetContractByIdQuery(id), ct)); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
    }

    [HttpPost("{id:guid}/start")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Start(Guid id, CancellationToken ct)
    {
        try { return Ok(await _mediator.Send(new StartContractCommand(id, GetCurrentUserId()), ct)); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpPost("{id:guid}/pause")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Pause(Guid id, CancellationToken ct)
    {
        try { return Ok(await _mediator.Send(new PauseContractCommand(id, GetCurrentUserId()), ct)); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
        catch (UnauthorizedAccessException) { return Forbid(); }
    }

    [HttpPost("{id:guid}/complete")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken ct)
    {
        try { return Ok(await _mediator.Send(new CompleteContractCommand(id, GetCurrentUserId()), ct)); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
        catch (UnauthorizedAccessException) { return Forbid(); }
    }

    [HttpPost("{id:guid}/milestones")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> AddMilestone(Guid id, [FromBody] CreateMilestoneRequestDto request, CancellationToken ct)
    {
        try { var userId = GetCurrentUserId(); var req = new CreateMilestoneRequest(id, request.Title, request.Description, request.Amount, request.DueDate); return Created("", await _mediator.Send(new AddMilestoneCommand(userId, req), ct)); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpPost("{contractId:guid}/milestones/{milestoneId:guid}/complete")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> CompleteMilestone(Guid contractId, Guid milestoneId, [FromBody] CompleteMilestoneRequest? request, CancellationToken ct)
    {
        try { return Ok(await _mediator.Send(new CompleteMilestoneCommand(milestoneId, GetCurrentUserId(), request?.Deliverables), ct)); }
        catch (InvalidOperationException ex) { return BadRequest(new { error = ex.Message }); }
    }

    [HttpGet("my-contracts")]
    public async Task<IActionResult> GetMyContracts(CancellationToken ct)
        => Ok(await _mediator.Send(new GetMyContractsQuery(GetCurrentUserId()), ct));

    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
    }
}

public record CreateMilestoneRequestDto(string Title, string Description, decimal Amount, DateTime? DueDate);
public record CompleteMilestoneRequest(string? Deliverables);
