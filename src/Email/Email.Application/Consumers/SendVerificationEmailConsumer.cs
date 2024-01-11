using MassTransit;
using Shared.Commands;

namespace Email.Application.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{

    public SendVerificationEmailConsumer()
    {
    }

    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
        string emailAddress = context.Message.Email;
    }
}