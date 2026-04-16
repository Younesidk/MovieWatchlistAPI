using Microsoft.EntityFrameworkCore;
using MovieWatchlistAPI.Data;
using MovieWatchlistAPI.DTO;
using MovieWatchlistAPI.Models;

namespace MovieWatchlistAPI.Repositories;

public class MovieRepository : IMoviesRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> AddMovie(MovieCreateDto movie)
    {
        var NewMovie = new Movie();

        NewMovie.Title = movie.Title;
        NewMovie.Genre = movie.Genre;
        NewMovie.ReleaseYear = movie.ReleaseYear;

        await _context.Movies.AddAsync(NewMovie);
        await _context.SaveChangesAsync();
        return NewMovie;
    }

    public async Task<bool> DeleteMovie(int id)
    {
        var movieToDelete = await _context.Movies.FindAsync(id);

        if (movieToDelete is null)
            return false;

        _context.Movies.Remove(movieToDelete);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Movie?> GetMovieById(int id)
    {
        var movie = await _context.Movies.FindAsync(id);
        return movie;
    }

    public async Task<List<Movie>> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return movies;
    }

    public async Task<bool> UpdateMovie(MovieUpdateDto movie,int id)
    {
        var movieToUpdate = await _context.Movies.FindAsync(id);

        if (movieToUpdate is null)
            return false;

        movieToUpdate.Title = movie.Title;
        movieToUpdate.Genre = movie.Genre;
        movieToUpdate.ReleaseYear = movie.ReleaseYear;

        _context.Movies.Update(movieToUpdate);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Movie?> UpdateRating(int id, int rating)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie is null)
            return null;

        movie.Rating = rating;

        _context.Movies.Update(movie);

        await _context.SaveChangesAsync();

        return movie;
    }
}