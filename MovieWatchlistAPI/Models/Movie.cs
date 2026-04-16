using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MovieWatchlistAPI.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    [Range(1900,2100)]
    public int ReleaseYear { get; set; }

    [Range(1, 10)] 
    public int Rating { get; set; }
}