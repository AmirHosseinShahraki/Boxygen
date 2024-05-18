using Email.Application.Commands;
using Email.Application.Messages;
using Email.Application.Services.Interfaces;
using Email.Domain;
using MassTransit;
using Shared.Events;

namespace Email.Application.Consumers;

public class VerifyEmailConsumer : IConsumer<VerifyEmail>
{
    private readonly IVerificationTokenValidator _tokenValidator;

    public VerifyEmailConsumer(IVerificationTokenValidator tokenValidator)
    {
        _tokenValidator = tokenValidator;
    }

    public async Task Consume(ConsumeContext<VerifyEmail> context)
    {
        VerifyEmail verifyEmailCommand = context.Message;
        VerificationToken? verificationToken = await _tokenValidator.Validate(verifyEmailCommand.Id,
            verifyEmailCommand.Email, verifyEmailCommand.Token);

        if (verificationToken is null)
        {
            await context.RespondAsync(new VerificationFailed());
            return;
        }

        EmailVerified emailVerified = new()
        {
            CorrelationId = verifyEmailCommand.Id
        };
        ISendEndpoint endpoint = await context.GetSendEndpoint(verificationToken.ResponseAddress);
        await endpoint.Send(emailVerified, c => c.RequestId = verificationToken.Id);
        await context.RespondAsync(emailVerified);
    }
}