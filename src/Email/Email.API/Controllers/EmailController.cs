using Email.Application.Commands;
using Email.Application.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Events;

namespace Email.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly IRequestClient<VerifyEmail> _emailVerificationClient;

    public EmailController(IRequestClient<VerifyEmail> emailVerificationClient)
    {
        _emailVerificationClient = emailVerificationClient;
    }

    [HttpGet]
    [Route("verify")]
    public async Task<IActionResult> Verify([FromQuery] Guid id, [FromQuery] string email, [FromQuery] string token)
    {
        VerifyEmail verifyEmailCommand = new(id, email, token);
        Response response =
            await _emailVerificationClient.GetResponse<EmailVerified, VerificationFailed>(verifyEmailCommand);

        return response switch
        {
            (_, EmailVerified) => Ok(),
            (_, VerificationFailed) => BadRequest(),
            _ => throw new InvalidOperationException()
        };
    }
}