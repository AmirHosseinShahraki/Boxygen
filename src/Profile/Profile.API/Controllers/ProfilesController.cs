using Microsoft.AspNetCore.Mvc;

namespace Profile.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
    [HttpGet("{userId}")]
    public IActionResult Get([FromRoute] string userId)
    {
        return Ok(new
        {
            Username = userId
        });
    }
}