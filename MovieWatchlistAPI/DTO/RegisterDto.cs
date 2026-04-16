using System.ComponentModel.DataAnnotations;

namespace MovieWatchlistAPI.DTO;

public class RegisterDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}