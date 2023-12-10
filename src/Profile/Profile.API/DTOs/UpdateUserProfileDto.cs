namespace Profile.API.DTOs;

public record UpdateUserProfileDto
{
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Phone { get; set; }
}