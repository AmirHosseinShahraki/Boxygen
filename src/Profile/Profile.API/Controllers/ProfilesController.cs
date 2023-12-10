using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Profile.API.DTOs;
using Profile.Domain.Commands;
using Profile.Domain.Entities;
using Profile.Domain.Messages;
using Profile.Domain.Queries;

namespace Profile.API.Controllers;

[ApiController]
[Route("[controller]/{userId:guid}")]
public class ProfilesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRequestClient<GetUserProfile> _getUserProfileClient;
    private readonly IRequestClient<UpdateUserProfile> _updateUserProfileClient;

    public ProfilesController(IMapper mapper, IRequestClient<GetUserProfile> getUserProfileClient, IRequestClient<UpdateUserProfile> updateUserProfileClient)
    {
        _mapper = mapper;
        _getUserProfileClient = getUserProfileClient;
        _updateUserProfileClient = updateUserProfileClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        var getUserProfileQuery = new GetUserProfile
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

    [HttpPost]
    public async Task<IActionResult> Submit([FromRoute] Guid userId, [FromBody] SubmitUserProfileDto submitUserProfileDto)
    {
        var toBeUpdateUserProfile = _mapper.Map<UserProfile>(submitUserProfileDto);
        var updateUserProfileCommand = new UpdateUserProfile
        {
            UserProfileId = userId,
            Profile = toBeUpdateUserProfile
        };
        Response response = await _updateUserProfileClient.GetResponse<UserProfile, UserProfileNotFound>(updateUserProfileCommand);
    
        return response switch
        {
            (_, UserProfile registeredUser) => Ok(registeredUser),
            (_, UserProfileNotFound) => NotFound(),
            _ => throw new InvalidOperationException()
        };
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UpdateUserProfileDto updateUserProfileDto)
    {
        var toBeUpdateUserProfile = _mapper.Map<UserProfile>(updateUserProfileDto);
        var updateUserProfileCommand = new UpdateUserProfile
        {
            UserProfileId = userId,
            Profile = toBeUpdateUserProfile
        };
        Response response = await _updateUserProfileClient.GetResponse<UserProfile, UserProfileNotFound>(updateUserProfileCommand);
    
        return response switch
        {
            (_, UserProfile registeredUser) => Ok(registeredUser),
            (_, UserProfileNotFound) => NotFound(),
            _ => throw new InvalidOperationException()
        };
    }
}