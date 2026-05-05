using DEPI.Application.DTOs.Media;
using DEPI.Application.UseCases.Media.CreateMediaFile;
using DEPI.Application.UseCases.Media.DeleteMediaFile;
using DEPI.Application.UseCases.Media.GetMediaFiles;
using DEPI.Application.UseCases.Media.GetAvatar;
using DEPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace DEPI.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MediaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MediaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("my-files")]
    [ProducesResponseType(typeof(IEnumerable<MediaFileResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserFiles([FromQuery] MediaType? type = null, CancellationToken cancellationToken = default)
    {
        var userId = GetCurrentUserId();
        var query = new GetMediaFilesQuery(userId, type, true);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("avatar")]
    [ProducesResponseType(typeof(MediaFileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAvatar(CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var query = new GetAvatarQuery(userId);
        var result = await _mediator.Send(query, cancellationToken);
        
        if (result == null)
            return NotFound(new { error = "Avatar not found" });

        return Ok(result);
    }

    [HttpPost("upload")]
    [ProducesResponseType(typeof(MediaFileResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromBody] UploadMediaFileRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = GetCurrentUserId();
            var fileExtension = Path.GetExtension(request.OriginalName);
            var command = new CreateMediaFileCommand(
                request.FileName,
                request.OriginalName,
                request.FilePath,
                fileExtension,
                request.FileSize,
                request.MimeType,
                request.Type,
                userId,
                request.Description
            );

            var result = await _mediator.Send(command, cancellationToken);
            return Created($"api/media/{result.Id}", result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteMediaFileCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("userId")?.Value;
        return Guid.Parse(userIdClaim ?? throw new InvalidOperationException("User ID not found"));
    }
}

public record UploadMediaFileRequest(
    string FileName,
    string OriginalName,
    string FilePath,
    long FileSize,
    string MimeType,
    MediaType Type,
    string? Description);