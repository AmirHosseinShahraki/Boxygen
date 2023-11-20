namespace Authentication.Domain.Messages;

public record AuthToken
{
    public string AccessToken { get; init; } = null!;
    public DateTime ExpiresAt { get; init; }
};