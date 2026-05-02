using Microsoft.AspNetCore.Mvc;
using MediatR;
using DEPI.Application.UseCases.Messaging.Queries;
namespace Depi.API.Controllers.Massaging
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetMessagesController : ControllerBase
    {
        private readonly ISender _sender;
        public GetMessagesController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> Handle([FromQuery] GetMessagesQuery query)
        {
            var messages = await _sender.Send(query);

            return Ok(messages.ToMessageResponseList());
        }
    }
}