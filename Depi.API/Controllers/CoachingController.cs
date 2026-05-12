using DEPI.Application.UseCases.Coaching;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoachingController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoachingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my-sessions")]
    public async Task<IActionResult> GetMySessions(Guid userId)
    {
        var result = await _mediator.Send(new GetMySessionsQuery(userId));
        return Ok(result);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming(Guid userId)
    {
        var result = await _mediator.Send(new GetUpcomingSessionsQuery(userId));
        return Ok(result);
    }

    [HttpGet("coaches")]
    public async Task<IActionResult> GetCoaches()
    {
        var result = await _mediator.Send(new GetCoachesQuery());
        return Ok(result);
    }

    [HttpPost("schedule")]
    public async Task<IActionResult> Schedule(Guid studentId, [FromBody] ScheduleSessionRequest request)
    {
        var result = await _mediator.Send(new ScheduleSessionCommand(studentId, request));
        return Ok(result);
    }

    [HttpPost("complete")]
    public async Task<IActionResult> Complete(Guid coachId, [FromBody] CompleteSessionRequest request)
    {
        var result = await _mediator.Send(new CompleteSessionCommand(coachId, request));
        return Ok(result);
    }

    [HttpPost("register-coach")]
    public async Task<IActionResult> RegisterCoach(Guid userId, [FromBody] RegisterCoachRequest request)
    {
        var result = await _mediator.Send(new RegisterCoachCommand(userId, request));
        return Ok(result);
    }
}