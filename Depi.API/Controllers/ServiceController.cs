using DEPI.Application.DTOs.Profiles;
using DEPI.Application.UseCases.Profiles.CreateServicePackage;
using DEPI.Application.UseCases.Profiles.DeleteServicePackage;
using DEPI.Application.UseCases.Profiles.GetServicePackagesByUser;
using DEPI.Application.UseCases.Profiles.SetServicePackageActive;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/service-packages")]
public class ServicePackagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicePackagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Create Service Package
    [HttpPost]
    public async Task<ActionResult<ServicePackageResponse>> Create([FromBody] CreateServicePackageCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByUserId), new { userId = result.UserId }, result);
    }

    // Get all packages 
    [HttpGet("user/{userId:guid}")]
    public async Task<ActionResult<IEnumerable<ServicePackageResponse>>> GetByUserId(Guid userId)
    {
        var result = await _mediator.Send(new GetServicePackagesByUserQuery(userId));
        return Ok(result);
    }

    // Delete service package
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteServicePackageCommand(id));
        return result ? NoContent() : NotFound();
    }

    // Activate / Deactivate service package
    [HttpPatch("{id:guid}/active")]
    public async Task<IActionResult> SetActive(Guid id, [FromQuery] bool isActive)
    {
        var result = await _mediator.Send(new SetServicePackageActiveCommand(id, isActive));
        return result ? Ok() : NotFound();
    }
}