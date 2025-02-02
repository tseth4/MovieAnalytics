using System.ComponentModel.DataAnnotations;

namespace MovieAnalytics.API.DTOs;

public class RegisterDto
{
    [Required]
    [MaxLength(100)]
    public string? Username { get; set; } = string.Empty;
    
    [Required, EmailAddress]
    public string? Email { get; set; }


    [Required] 
    public string? KnownAs { get; set; }
    
    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;

}