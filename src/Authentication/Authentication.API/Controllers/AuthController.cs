using Authentication.API.DTOs;
using Authentication.Domain.Commands;
using Shared.Events;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IRequestClient<RegisterUser> _registrationClient;

    public AuthController(IMapper mapper, IRequestClient<RegisterUser> registrationClient) : base(mapper)
    {
        _registrationClient = registrationClient;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationDto registrationDto)
    {
        var registerUserCommand = Mapper.Map<RegisterUser>(registrationDto);
        var response = await _registrationClient.GetResponse<NewUserRegistered>(registerUserCommand);
        return Ok(response.Message);
    }
}