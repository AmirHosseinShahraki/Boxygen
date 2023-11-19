namespace Authentication.Domain.Commands;

public record RegisterUser
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}