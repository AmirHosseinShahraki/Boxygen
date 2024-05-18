namespace Email.Infrastructure.Helpers;

public class SmtpConfig
{
    public string Host { get; init; } = null!;
    public int Port { get; init; }
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string DefaultFrom { get; init; } = null!;
    public string DefaultName { get; init; } = null!;
}