namespace Authentication.Domain.Messages;

public record AuthFailed
{
    public string Username { get; set; } = null!;
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
};