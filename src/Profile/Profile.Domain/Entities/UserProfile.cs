namespace Profile.Domain.Entities;

public class UserProfile
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? ImageUri { get; set; }
}