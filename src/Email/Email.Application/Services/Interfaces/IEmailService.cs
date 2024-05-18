namespace Email.Application.Services.Interfaces;

public interface IEmailService
{
    public Task Send(string to, string subject, string body);
}