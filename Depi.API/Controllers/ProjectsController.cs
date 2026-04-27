using DEPI.Application.UseCases.Projects.CreateProject;
using DEPI.Application.UseCases.Projects.UpdateProject;
using DEPI.Application.UseCases.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Create
    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    // Get by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProjectQuery(id));

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    // Get All (with filters)
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetProjectsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // Update
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectCommand command)
    {
        if (id != command.ProjectId)
            return BadRequest("Id mismatch");

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}