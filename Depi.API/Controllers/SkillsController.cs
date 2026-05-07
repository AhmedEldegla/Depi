using DEPI.Application.DTOs.Profiles;
using DEPI.Application.UseCases.Profiles.CreateSkill;
using DEPI.Application.UseCases.Profiles.UpdateSkill;
using DEPI.Application.UseCases.Profiles.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SkillsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] bool? isActive, CancellationToken ct)
        => Ok(await _mediator.Send(new GetAllSkillsQuery(isActive), ct));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateSkillDto dto, CancellationToken ct)
        => Created("", await _mediator.Send(new CreateSkillCommand(dto.Name, dto.NameEn, dto.Description, dto.IsVerified, dto.DisplayOrder), ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSkillDto dto, CancellationToken ct)
        => Ok(await _mediator.Send(new UpdateSkillCommand(id, dto.Name, dto.NameEn, dto.Description, dto.IsVerified, dto.IsActive, dto.DisplayOrder), ct));
}

public record CreateSkillDto(string Name, string? NameEn, string? Description, bool IsVerified = false, int? DisplayOrder = null);
public record UpdateSkillDto(string Name, string NameEn, string? Description, bool IsVerified, bool IsActive, int DisplayOrder);
