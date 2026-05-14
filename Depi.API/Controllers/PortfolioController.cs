using DEPI.Application.DTOs.Profiles;
using DEPI.Application.UseCases.Profiles.CreatePortfolioItem;
using DEPI.Application.UseCases.Profiles.DeletePortfolioItem;
using DEPI.Application.UseCases.Profiles.GetFeaturedPortfolio;
using DEPI.Application.UseCases.Profiles.GetPortfolioItemsByUser;
using DEPI.Application.UseCases.Profiles.PublishPortfolioItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/portfolio-items")]
public class PortfolioItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PortfolioItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Create portfolio item
    [HttpPost]
    public async Task<ActionResult<PortfolioItemResponse>> Create([FromBody] CreatePortfolioItemCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByUserId), new { userId = result.UserId }, result);
    }

    // Get portfolio items by user
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<PortfolioItemResponse>>> GetByUserId(Guid userId)
    {
        var result = await _mediator.Send(new GetPortfolioItemsByUserQuery(userId));
        return Ok(result);
    }

    // Get featured portfolio items
    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<PortfolioItemResponse>>> GetFeatured()
    {
        var result = await _mediator.Send(new GetFeaturedPortfolioQuery());
        return Ok(result);
    }

    // Publish portfolio item
    [HttpPatch("{id:guid}/publish")]
    public async Task<IActionResult> Publish(Guid id)
    {
        var result = await _mediator.Send(new PublishPortfolioItemCommand(id));
        return result ? Ok() : NotFound();
    }

    // Delete portfolio item
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeletePortfolioItemCommand(id));
        return result ? NoContent() : NotFound();
    }
}