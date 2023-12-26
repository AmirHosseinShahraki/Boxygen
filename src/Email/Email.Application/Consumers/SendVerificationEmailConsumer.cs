using FluentEmail.Core;
using MassTransit;
using Shared.Commands;

namespace Email.Application.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{
    private readonly IFluentEmailFactory _emailFactory;

    public SendVerificationEmailConsumer(IFluentEmailFactory emailFactory)
    {
        _emailFactory = emailFactory;
    }

    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
        var emailAddress = context.Message.Email;
        var email = _emailFactory
            .Create()
            .To(emailAddress)
            .Subject("Email Verification")
            .Body("Please click the link below to verify your email address");
        await email.SendAsync();
    }
}