using DEPI.Application.UseCases.Guild.Queries.GetGuild;
using DEPI.Application.UseCases.Guilds.Commands.CreateGuild;
using DEPI.Application.UseCases.Guilds.Commands.JoinGuild;
using DEPI.Application.UseCases.Guilds.Commands.LeaveGuild;
using DEPI.Application.UseCases.Guilds.Commands.UpdateGuild;
using DEPI.Application.UseCases.Guilds.Contracts;
using DEPI.Application.UseCases.Guilds.Queries.GetGuilds;
using DEPI.Application.UseCases.Guilds.Queries.GetMyGuilds;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class GuildsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuildsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all guilds or filter by specialization
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? specialization,
        CancellationToken cancellationToken)
    {
        var guilds = await _mediator.Send(
            new GetGuildsQuery(specialization),
            cancellationToken);

        return Ok(guilds);
    }

    /// <summary>
    /// Get guild by id
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var guild = await _mediator.Send(
            new GetGuildQuery(id),
            cancellationToken);

        if (guild is null)
        {
            return NotFound(new
            {
                Message = "Guild not found"
            });
        }

        return Ok(guild);
    }

    /// <summary>
    /// Get user guilds
    /// </summary>
    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyGuilds(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var guilds = await _mediator.Send(
            new GetMyGuildsQuery(userId),
            cancellationToken);

        return Ok(guilds);
    }

    /// <summary>
    /// Create new guild
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromQuery] Guid leaderId,
        [FromBody] CreateGuildRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var createdGuild = await _mediator.Send(
            new CreateGuildCommand(leaderId, request),
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdGuild.Id },
            createdGuild);
    }

    /// <summary>
    /// Update guild data
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromQuery] Guid userId,
        [FromBody] UpdateGuildRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            var updatedGuild = await _mediator.Send(
                new UpdateGuildCommand(id, userId, request),
                cancellationToken);

            return Ok(updatedGuild);
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Join guild
    /// </summary>
    [HttpPost("{id:guid}/join")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Join(
        Guid id,
        [FromQuery] Guid userId,
        [FromQuery] string skills,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new JoinGuildCommand(id, userId, skills),
                cancellationToken);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Leave guild
    /// </summary>
    [HttpDelete("{id:guid}/leave")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Leave(
        Guid id,
        [FromQuery] Guid userId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new LeaveGuildCommand(id, userId),
            cancellationToken);

        return NoContent();
    }
}