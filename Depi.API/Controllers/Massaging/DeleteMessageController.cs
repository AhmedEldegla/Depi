using Microsoft.AspNetCore.Mvc;
using MediatR;
using DEPI.Application.UseCases.Messaging.Commands;
namespace Depi.API.Controllers.Massaging
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteMessageController : ControllerBase
    {
        private readonly ISender _sender;
        public DeleteMessageController(ISender sender) => _sender = sender;

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Handle(Guid id)
        {
            var command = new DeleteMessageCommand(id);
            var result = await _sender.Send(command);

            return result.IsSuccess ? Ok() : NotFound(result.Error);
        }
    }
}