using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieWatchlistAPI.DTO;
using MovieWatchlistAPI.Models;
using MovieWatchlistAPI.Repositories;

namespace MovieWatchlistAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMoviesRepository _repo;

    public MoviesController(IMoviesRepository repo)
    {
        _repo = repo;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<MovieResponseDto>>> GetMovies()
    {
        var movies = await _repo.GetMovies();

        var mappedMovies = new List<MovieResponseDto>();

        foreach (var movie in movies)
        {
            var mappedMovie = MapToDto(movie);
            mappedMovies.Add(mappedMovie);
        }

        return Ok(mappedMovies);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieResponseDto>> GetMovieById(int id)
    {
        var movie = await _repo.GetMovieById(id);

        if (movie is null)
            return NotFound();

        var mappedMovie = MapToDto(movie);

        return Ok(mappedMovie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> AddMovie([FromBody] MovieCreateDto movie)
    {
        var NewMovie = await _repo.AddMovie(movie);

        return CreatedAtAction("GetMovieById", new { id = NewMovie.Id }, movie);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMovie([FromBody] MovieUpdateDto movie,int id)
    {
        var IsUpdated = await _repo.UpdateMovie(movie,id);

        if (!IsUpdated)
            return NotFound();

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMovie(int id)
    {
        var deleted = await _repo.DeleteMovie(id);

        if (!deleted)
            return NotFound("Movie Was Not Found");

        return NoContent();
    }

    [HttpPatch("{id}/rating")]
    public async Task<ActionResult<MovieResponseDto>> UpdateRating([FromBody] RatingDto rating, int id)
    {
        var updatedMovie = await _repo.UpdateRating(id, rating.Rating);

        if (updatedMovie is null)
            return NotFound();

        var mappedMovie = MapToDto(updatedMovie);

        return Ok(mappedMovie);
    }

    private MovieResponseDto MapToDto(Movie movie)
    {
        var movieDto = new MovieResponseDto();
        movieDto.Title = movie.Title;
        movieDto.Genre = movie.Genre;
        movieDto.Rating = movie.Rating;
        movieDto.ReleaseYear = movie.ReleaseYear;

        return movieDto;
    }
}