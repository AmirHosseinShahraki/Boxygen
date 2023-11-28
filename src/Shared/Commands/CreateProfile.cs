namespace Shared.Commands;

public record CreateProfile
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
}