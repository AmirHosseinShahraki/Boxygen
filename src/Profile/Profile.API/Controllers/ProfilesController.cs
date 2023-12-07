using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Profile.Domain.Entities;
using Profile.Domain.Messages;
using Profile.Domain.Queries;

namespace Profile.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly IRequestClient<GetUserProfile> _getUserProfileClient;

    public ProfilesController(IRequestClient<GetUserProfile> getUserProfileClient)
    {
        _getUserProfileClient = getUserProfileClient;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        var getUserProfileQuery = new GetUserProfile()
        {
            Id = userId
        };
        Response response = await _getUserProfileClient.GetResponse<UserProfile, UserProfileNotFound>(getUserProfileQuery);

        return response switch
        {
            (_, UserProfile registeredUser) => Ok(registeredUser),
            (_, UserProfileNotFound) => NotFound(),
            _ => throw new InvalidOperationException()
        };
    }
}