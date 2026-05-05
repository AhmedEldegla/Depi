using DEPI.Application.DTOs.Projects;
using DEPI.Application.UseCases.Projects.CreateProject;
using DEPI.Application.UseCases.Projects.DeleteProject;
using DEPI.Application.UseCases.Projects.OpenProject;
using DEPI.Application.UseCases.Projects.UpdateProject;
using DEPI.Application.UseCases.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProjectsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProjects([FromQuery] ProjectFilterRequest filter, CancellationToken ct)
    {
        var query = new GetProjectsQuery(filter.Status, filter.Type, filter.RequiredLevel, filter.CategoryId, filter.MinBudget, filter.MaxBudget, filter.Search, null, filter.Page, filter.PageSize);
        var result = await _mediator.Send(query, ct);
        return result.IsFailure ? BadRequest(new { error = result.Error }) : Ok(result.Value);
    }

    [HttpGet("my-projects")]
    [Authorize(Roles = "Admin,Client,Freelancer")]
    public async Task<IActionResult> GetMyProjects([FromQuery] ProjectFilterRequest filter, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var query = new GetProjectsQuery(filter.Status, filter.Type, filter.RequiredLevel, filter.CategoryId, filter.MinBudget, filter.MaxBudget, filter.Search, userId, filter.Page, filter.PageSize);
        var result = await _mediator.Send(query, ct);
        return result.IsFailure ? BadRequest(new { error = result.Error }) : Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetProjectQuery(id), ct);
        return result.IsFailure ? NotFound(new { error = result.Error }) : Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Create([FromBody] CreateProjectRequest request, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var command = new CreateProjectCommand(userId, request.Title, request.Description, request.Type, request.BudgetMin, request.BudgetMax, request.FixedPrice, request.RequiredLevel, request.Deadline ?? DateTime.UtcNow.AddDays(30), request.Skills, request.IsNda, request.CategoryId);
        var result = await _mediator.Send(command, ct);
        return result.IsFailure ? BadRequest(new { error = result.Error }) : Created("", result.Value);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectRequest request, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var command = new UpdateProjectCommand(userId, id, request.Title, request.Description, request.BudgetMin, request.BudgetMax, request.Deadline, request.Skills, request.RequiredLevel, request.IsNda);
        var result = await _mediator.Send(command, ct);
        return result.IsFailure ? BadRequest(new { error = result.Error }) : Ok(result.Value);
    }

    [HttpPost("{id:guid}/open")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Open(Guid id, CancellationToken ct)
    {
        var userId = GetCurrentUserId();
        var result = await _mediator.Send(new OpenProjectCommand(userId, id), ct);
        return result.IsFailure ? BadRequest(new { error = result.Error }) : Ok(result.Value);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin,Client")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        try { var userId = GetCurrentUserId(); await _mediator.Send(new DeleteProjectCommand(id, userId), ct); return NoContent(); }
        catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        catch (UnauthorizedAccessException) { return Forbid(); }
    }

    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
    }
}
