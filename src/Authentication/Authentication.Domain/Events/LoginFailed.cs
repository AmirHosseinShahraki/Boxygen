namespace Authentication.Domain.Events;

public record LoginFailed
{
    public string Username { get; set; } = null!;
    public DateTime Time { get; set; }
};