namespace Authentication.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}