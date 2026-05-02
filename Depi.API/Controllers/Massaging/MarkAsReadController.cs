using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace Depi.API.Controllers.Massaging
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkAsReadController : ControllerBase
    {
        private readonly ISender _sender;
        public MarkAsReadController(ISender sender) => _sender = sender;

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Handle(Guid id)
        {
        
            var command = new MarkAsReadCommand(id);

            var result = await _sender.Send(command);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}