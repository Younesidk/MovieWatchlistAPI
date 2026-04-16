using System.ComponentModel.DataAnnotations;

namespace MovieWatchlistAPI.DTO;

public class LoginDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Username { get; set; }
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Password { get; set; }
}