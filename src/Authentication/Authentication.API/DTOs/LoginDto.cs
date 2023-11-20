namespace Authentication.API.DTOs;

public record LoginDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}