using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieWatchlistAPI.Models;


public class WatchlistItem
{
    public int Id { get; set; }

    public status Status { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public IdentityUser User { get; set; }

    [MaxLength(100)]
    public string? PersonalNote { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public Movie Movie { get; set; }
}