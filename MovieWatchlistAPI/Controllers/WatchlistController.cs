using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieWatchlistAPI.DTO;
using MovieWatchlistAPI.Models;
using MovieWatchlistAPI.Repositories;

namespace MovieWatchlistAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WatchlistController : ControllerBase
{
    private readonly IWatchlistItemRepository _repo;
    private readonly UserManager<IdentityUser> _userManager;

    public WatchlistController(IWatchlistItemRepository repo,UserManager<IdentityUser> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<WatchlistItemResponseDto>>> GetAllWatchListItems()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        if (user is null)
            return Unauthorized();

        var userId = user.Id;

        var items = await _repo.GetAllWatchListItems(userId);

        var mappedItems = new List<WatchlistItemResponseDto>();

        foreach (var item in items)
        {
            mappedItems.Add(MapToDto(item));
        }

        return Ok(mappedItems);
    }

    [HttpPost("{movieId}")]
    public async Task<ActionResult<WatchlistItemResponseDto>> AddWatchlistItem(int movieId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user is null)
            return Unauthorized();

        var item = await _repo.AddMovieToWatchList(movieId, user);

        if (item is null)
            return NotFound("movie doesn't exist");

        var mappedItem = MapToDto(item);

        return Ok(mappedItem);
    }

    [HttpPatch("{movieId}/status")]
    public async Task<ActionResult<WatchlistItemResponseDto>> UpdateWatchStatus(int movieId, [FromBody] UpdateStatusDto dto)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user is null)
            return Unauthorized();

        var item = await _repo.UpdateWatchStatus(movieId, dto.Status, user);

        if (item is null)
            return NotFound();

        var mappedItem = MapToDto(item);

        return Ok(mappedItem);
    }

    [HttpDelete("{movieId}")]
    public async Task<ActionResult> DeleteWatchlistItem(int movieId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user is null)
            return Unauthorized();

        var deleted = await _repo.RemoveFromWatchList(movieId, user);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpPut("{movieId}/PersonalNote")]
    public async Task<ActionResult<WatchlistItemResponseDto>> UpdatePersonalNote([FromBody] PersonalNoteDto note, int movieId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user is null)
            return Unauthorized();

        var item = await _repo.UpdatePersonalNote(movieId, user, note.PersonalNote);

        if (item is null)
            return NotFound();

        var mappedItem = MapToDto(item);

        return Ok(mappedItem);
    }

    private WatchlistItemResponseDto MapToDto(WatchlistItem item)
    {
        var mappedItem = new WatchlistItemResponseDto();

        var mappedMovie = new MovieResponseDto();

        mappedMovie.Genre = item.Movie.Genre;
        mappedMovie.Title = item.Movie.Title;
        mappedMovie.ReleaseYear = item.Movie.ReleaseYear;
        mappedMovie.Rating = item.Movie.Rating;

        mappedItem.Movie = mappedMovie;
        mappedItem.PersonalNote = item.PersonalNote;
        mappedItem.Status = item.Status;

        return mappedItem;
    }
}