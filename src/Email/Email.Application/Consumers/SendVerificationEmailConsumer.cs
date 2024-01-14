using Email.Application.Services;
using Email.Domain.Enums;
using MassTransit;
using Shared.Commands;

namespace Email.Application.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{
    private readonly IEmailService _emailService;
    private readonly ITemplateProvider _templateProvider;

    public SendVerificationEmailConsumer(IEmailService emailService, ITemplateProvider templateProvider)
    {
        _emailService = emailService;
        _templateProvider = templateProvider;
    }

    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
        string emailContent = _templateProvider.Render(Template.Verification, context.Message);
        await _emailService.Send(context.Message.Email, "Please verify your account", emailContent);
    }
}