using Microsoft.AspNetCore.Mvc;

namespace Email.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    [HttpGet]
    [Route("verify")]
    public Task<IActionResult> Verify([FromQuery] string token, [FromQuery] string email)
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}