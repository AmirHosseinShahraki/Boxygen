namespace Shared.Events;

public record NewUserRegistered
{
    public string Id { get; init; } = null!;
    public string Username { get; init; } = null!;
    public DateTimeOffset RegisteredAt { get; init; } = DateTimeOffset.UtcNow;
};