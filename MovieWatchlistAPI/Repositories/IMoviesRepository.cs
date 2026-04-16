using MovieWatchlistAPI.DTO;
using MovieWatchlistAPI.Models;

namespace MovieWatchlistAPI.Repositories;

public interface IMoviesRepository
{
    public Task<List<Movie>> GetMovies();

    public Task<Movie?> GetMovieById(int id);

    public Task<Movie> AddMovie(MovieCreateDto movie);

    public Task<bool> DeleteMovie(int id);

    public Task<bool> UpdateMovie(MovieUpdateDto movie,int id);

    public Task<Movie?> UpdateRating(int id, int rating);
}