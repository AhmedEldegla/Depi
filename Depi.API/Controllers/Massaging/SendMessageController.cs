using Microsoft.AspNetCore.Mvc;
using MediatR;
using DEPI.Application.UseCases.Messaging.Commands;
namespace Depi.API.Controllers.Massaging
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendMessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SendMessageController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Handle([FromBody] SendMessageCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}