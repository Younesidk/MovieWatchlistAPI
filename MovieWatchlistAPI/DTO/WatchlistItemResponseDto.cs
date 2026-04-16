using Microsoft.AspNetCore.Identity;
using MovieWatchlistAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieWatchlistAPI.DTO;

public class WatchlistItemResponseDto
{
    public status Status { get; set; }

    public string PersonalNote { get; set; }

    public MovieResponseDto Movie { get; set; }
}