namespace Authentication.Domain.Events;

public record AuthTokenGenerated
{
    public string AccessToken { get; init; } = null!;
    public DateTime ExpiresAt { get; set; }
};