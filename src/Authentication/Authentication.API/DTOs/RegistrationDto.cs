using System.ComponentModel.DataAnnotations;

namespace Authentication.API.DTOs;

public record RegistrationDto
{
    [Required]
    public string Username { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}