namespace Authentication.Domain.Entities;

public class Credential
{
    public Guid Id { get; set; }
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}