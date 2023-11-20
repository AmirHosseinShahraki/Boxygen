using Authentication.API.DTOs;
using Authentication.Domain.Commands;
using Authentication.Domain.Events;
using Shared.Events;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly IRequestClient<RegisterUser> _registrationClient;
    private readonly IRequestClient<LoginUser> _loginClient;

    public AuthController(IMapper mapper, IRequestClient<RegisterUser> registrationClient,
        IRequestClient<LoginUser> loginClient) : base(mapper)
    {
        _registrationClient = registrationClient;
        _loginClient = loginClient;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegistrationDto registrationDto)
    {
        var registerUserCommand = Mapper.Map<RegisterUser>(registrationDto);
        var response = await _registrationClient.GetResponse<NewUserRegistered>(registerUserCommand);
        return Ok(response.Message);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var loginUserCommand = Mapper.Map<LoginUser>(loginDto);
        Response response = await _loginClient.GetResponse<AuthTokenGenerated, LoginFailed>(loginUserCommand);

        return response switch
        {
            (_, AuthTokenGenerated authTokenGenerated) => Ok(authTokenGenerated),
            (_, LoginFailed) => Unauthorized(),
            _ => throw new InvalidOperationException()
        };
    }
}