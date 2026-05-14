using MediatR;
using Microsoft.AspNetCore.Mvc;
using DEPI.Application.UseCases.Companies.Contracts;
using DEPI.Application.UseCases.Companies.Commands.CreateCompany;
using DEPI.Application.UseCases.Companies.Commands.DeleteCompany;
using DEPI.Application.UseCases.Companies.Commands.UpdateCompany;
using DEPI.Application.UseCases.Companies.Queries.GetCompany;
using Depi.Application.UseCases.Companies.Queries.ListCompanies;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CompanyResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompanyResponse>>> GetAll(
        [FromQuery] string? searchTerm,
        [FromQuery] bool? verifiedOnly,
        CancellationToken ct)
    {
        var query = new ListCompaniesQuery(searchTerm, verifiedOnly);
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCompanyQuery(id), ct);

        if (result is null)
            return NotFound(new ProblemDetails { Title = "Company not found" });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<CompanyResponse>> Create(
        [FromBody] CreateCompanyRequest request,
        CancellationToken ct)
    {
        var ownerId = Guid.NewGuid(); 

        var result = await _mediator.Send(new CreateCompanyCommand(ownerId, request), ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CompanyResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<CompanyResponse>> Update(
        Guid id,
        [FromBody] UpdateCompanyRequest request,
        CancellationToken ct)
    {
        var userId = Guid.NewGuid();

        var result = await _mediator.Send(new UpdateCompanyCommand(id, userId, request), ct);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var userId = Guid.NewGuid(); 

        await _mediator.Send(new DeleteCompanyCommand(id, userId), ct);

        return NoContent();
    }
}