using FluentEmail.Core;
using MassTransit;
using Shared.Commands;

namespace Email.Application.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{
    private readonly IFluentEmail _fluentEmail;

    public SendVerificationEmailConsumer(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
        var emailAddress = context.Message.Email;
        var email = _fluentEmail
            .To(emailAddress)
            .Subject("Email Verification")
            .Body("Please click the link below to verify your email address");
        await email.SendAsync();
    }
}