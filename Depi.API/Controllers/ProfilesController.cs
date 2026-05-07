using DEPI.Application.DTOs.Profiles;
using DEPI.Application.UseCases.Profiles.CreateUserProfile;
using DEPI.Application.UseCases.Profiles.UpdateUserProfile;
using DEPI.Application.UseCases.Profiles.GetUserProfileByUserId;
using DEPI.Application.UseCases.Profiles.SetUserProfileAvailability;
using DEPI.Application.UseCases.Profiles.GetAvailableFreelancers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfilesController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProfilesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin,Freelancer,Student,Coach,HeadHunter")]
    public async Task<IActionResult> Create([FromBody] CreateProfileDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateUserProfileCommand(GetCurrentUserId(), dto.DisplayName, dto.Title, dto.Bio, dto.HourlyRate, dto.CurrencyId), ct);
        return result.IsSuccess ? Created("", result.Value) : BadRequest(new { error = result.Error });
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile(CancellationToken ct)
        => Ok(await _mediator.Send(new GetUserProfileByUserIdQuery(GetCurrentUserId()), ct));

    [HttpGet("available")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailable([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        => Ok(await _mediator.Send(new GetAvailableFreelancersQuery(search, page, pageSize), ct));

    [HttpPut("me")]
    [Authorize(Roles = "Admin,Freelancer,Student,Coach,HeadHunter")]
    public async Task<IActionResult> Update([FromBody] UpdateProfileDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new UpdateUserProfileCommand(GetCurrentUserId(), dto.DisplayName, dto.Title, dto.Bio, dto.HourlyRate, dto.CountryId, dto.LinkedInUrl, dto.PortfolioUrl, dto.GithubUrl, dto.WebsiteUrl, dto.Gender, dto.BirthDate, dto.PhoneNumber, dto.Address), ct);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(new { error = result.Error });
    }

    [HttpPut("me/availability")]
    [Authorize(Roles = "Admin,Freelancer")]
    public async Task<IActionResult> SetAvailability([FromBody] SetAvailableDto dto, CancellationToken ct)
        => Ok(await _mediator.Send(new SetUserProfileAvailabilityCommand(GetCurrentUserId(), dto.IsAvailable), ct));

    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(sub, out var uid) ? uid : Guid.Empty;
    }
}

public record CreateProfileDto(string DisplayName, string Title, string Bio, decimal HourlyRate, Guid? CurrencyId);
public record UpdateProfileDto(string DisplayName, string Title, string Bio, decimal HourlyRate, Guid? CountryId, string? LinkedInUrl, string? PortfolioUrl, string? GithubUrl, string? WebsiteUrl, DEPI.Domain.Enums.Gender Gender, DateTime? BirthDate, string? PhoneNumber, string? Address);
public record SetAvailableDto(bool IsAvailable);
