using Email.Application.Services;
using Email.Infrastructure.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Email.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SMTPConfig _smtpConfig;
    private readonly SmtpClient _client = new();

    public EmailService(IOptions<SMTPConfig> configuration)
    {
        _smtpConfig = configuration.Value;

        _client.Connect(_smtpConfig.Host, _smtpConfig.Port);
        _client.Authenticate(_smtpConfig.Username, _smtpConfig.Password);
    }

    public async Task Send(string to, string subject, string body)
    {
        var message = new MimeMessage();
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