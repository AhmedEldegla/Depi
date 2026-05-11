using DEPI.Application.DTOs.Profiles;
using DEPI.Application.UseCases.Profiles.CreatePortfolioItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IMediator _mediator;

    public PortfolioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Create Portfolio Item
    [HttpPost]
    public async Task<ActionResult<PortfolioItemResponse>> Create(
        [FromBody] CreatePortfolioItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}