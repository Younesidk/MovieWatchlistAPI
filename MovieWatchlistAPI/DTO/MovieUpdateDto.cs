using System.ComponentModel.DataAnnotations;

namespace MovieWatchlistAPI.DTO;

public class MovieUpdateDto
{
    public string? Title { get; set; }

    public string? Genre { get; set; }

    public int ReleaseYear { get; set; }
}