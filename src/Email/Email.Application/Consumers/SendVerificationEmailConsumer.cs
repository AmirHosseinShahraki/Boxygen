using FluentEmail.Core;
using MassTransit;
using Shared.Commands;

namespace Email.Application.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{
    public SendVerificationEmailConsumer(IFluentEmail fluentEmail)
    {
    }

    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
    }
}