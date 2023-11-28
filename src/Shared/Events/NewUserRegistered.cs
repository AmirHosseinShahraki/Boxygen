namespace Shared.Events;

public record NewUserRegistered
{
    public Guid Id { get; init; }
    public string Username { get; init; } = null!;
    public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;
};