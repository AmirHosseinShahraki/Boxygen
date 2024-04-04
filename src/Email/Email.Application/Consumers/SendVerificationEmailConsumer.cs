using Email.Application.Services.Interfaces;
using Email.Domain;
using Email.Domain.Enums;
using MassTransit;
using Shared.Commands;

namespace Email.Application.Consumers;

public class SendVerificationEmailConsumer : IConsumer<SendVerificationEmail>
{
    private readonly IEmailService _emailService;
    private readonly IVerificationTokenManager _verificationTokenManager;
    private readonly ITemplateProvider _templateProvider;

    public SendVerificationEmailConsumer(IEmailService emailService, IVerificationTokenManager verificationTokenManager,
        ITemplateProvider templateProvider)
    {
        _emailService = emailService;
        _verificationTokenManager = verificationTokenManager;
        _templateProvider = templateProvider;
    }

    public async Task Consume(ConsumeContext<SendVerificationEmail> context)
    {
        VerificationToken verificationToken = await _verificationTokenManager.Generate(context.Message.Email);
        string emailContent = _templateProvider.Render(Templates.Verification, new
        {
            context.Message.FullName,
            verificationToken.HashedEmailAddress,
            VerificationToken = verificationToken.Token,
        });
        await _emailService.Send(context.Message.Email, "Please verify your email account", emailContent);
    }
}