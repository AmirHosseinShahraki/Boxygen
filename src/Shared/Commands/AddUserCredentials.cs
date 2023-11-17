namespace Shared.Commands;

public record AddUserCredentials
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
};