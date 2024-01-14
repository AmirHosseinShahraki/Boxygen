namespace Email.Application.Services;

public interface IEmailService
{
    public Task Send(string to, string subject, string body);
}