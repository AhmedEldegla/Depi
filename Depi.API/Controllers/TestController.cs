using Microsoft.AspNetCore.Mvc;

namespace Depi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API is working ");
    }
}