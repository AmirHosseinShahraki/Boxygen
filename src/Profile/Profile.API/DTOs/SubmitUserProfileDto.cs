namespace Profile.API.DTOs;

public record SubmitUserProfileDto
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; } = null!;
}