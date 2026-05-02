using Microsoft.AspNetCore.Mvc;
using MediatR;
using DEPI.Application.UseCases.Messaging.Queries;
namespace Depi.API.Controllers.Massaging
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetNotificationsController : ControllerBase
    {
        private readonly ISender _sender;
        public GetNotificationsController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<IActionResult> Handle()
        {
            var query = new GetNotificationsQuery();
            var notifications = await _sender.Send(query);

            return Ok(notifications);
        }
    }
}