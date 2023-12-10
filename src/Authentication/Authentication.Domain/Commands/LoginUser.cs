namespace Authentication.Domain.Commands;

public record LoginUser
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}