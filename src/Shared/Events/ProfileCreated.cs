namespace Shared.Events;

public record ProfileCreated
{
    public Guid Id { get; init; }
    public string Username { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
}