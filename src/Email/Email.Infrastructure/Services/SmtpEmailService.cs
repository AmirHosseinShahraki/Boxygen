using Email.Application.Services;
using Email.Application.Services.Interfaces;
using Email.Infrastructure.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Email.Infrastructure.Services;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpConfig _smtpConfig;
    private readonly SmtpClient _client = new();

    public SmtpEmailService(IOptions<SmtpConfig> configuration)
    {
        _smtpConfig = configuration.Value;

        _client.Connect(_smtpConfig.Host, _smtpConfig.Port);
        _client.Authenticate(_smtpConfig.Username, _smtpConfig.Password);
    }

    public async Task Send(string to, string subject, string body)
    {
        MimeMessage message = new();
        message.From.Add(new MailboxAddress(_smtpConfig.DefaultName, _smtpConfig.DefaultFrom));
        message.To.Add(new MailboxAddress(null, to));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html)
        {
            Text = body
        };
        await _client.SendAsync(message);
    }
}