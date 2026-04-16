using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieWatchlistAPI.Data;
using MovieWatchlistAPI.Models;

namespace MovieWatchlistAPI.Repositories;

public class WatchlistItemRepository : IWatchlistItemRepository
{
    private readonly AppDbContext _context;

    public WatchlistItemRepository(AppDbContext context)
    {
        _context = context;
    }

    //GET api/watchlist
    public async Task<List<WatchlistItem>> GetAllWatchListItems(string userId)
    {
        var items = await _context.WatchlistItem
            .Include(w => w.Movie)
            .Where(w => w.UserId == userId)
            .ToListAsync<WatchlistItem>();

        return items;
    }

    //POST api/watchlist/{movieId}
    public async Task<WatchlistItem?> AddMovieToWatchList(int id,IdentityUser user)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie is null)
            return null;

        var watchlistItem = new WatchlistItem();

        var userId = user.Id;

        watchlistItem.User = user;
        watchlistItem.Movie = movie;
        watchlistItem.UserId = userId;

        await _context.WatchlistItem.AddAsync(watchlistItem);

        await _context.SaveChangesAsync();

        return watchlistItem;
    }

    //PATCH api/watchlist/{movieId}/status
    public async Task<WatchlistItem?> UpdateWatchStatus(int id, string status,IdentityUser user)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie is null)
            return null;

        var userId = user.Id;

        var watchlistItem = await _context.WatchlistItem
            .FirstOrDefaultAsync(w => w.MovieId == id && w.UserId == userId);

        if (watchlistItem is null)
            return null;

        status? enumStatus = status switch
        {
            "PlanToWatch" => Models.status.PlanToWatch,
            "Watching" => Models.status.Watching,
            "Completed" => Models.status.Completed,
            "Dropped" => Models.status.Dropped,
            _ => null
        };

        if (enumStatus is null)
            return null;

        watchlistItem.Status = (status)enumStatus;

        _context.WatchlistItem.Update(watchlistItem);

        await _context.SaveChangesAsync();

        return watchlistItem;
    }

    //DELETE /api/watchlist/{movieId}
    public async Task<bool> RemoveFromWatchList(int id, IdentityUser user)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie is null)
            return false;

        var userId = user.Id;

        var watchlistItem = await _context.WatchlistItem.FindAsync(movie.Id, userId);

        if (watchlistItem is null)
            return false;

        _context.WatchlistItem.Remove(watchlistItem);

        await _context.SaveChangesAsync();

        return true;
    }

    //PUT /api/watchlist/{movieId}/PersonalNote
    public async Task<WatchlistItem?> UpdatePersonalNote(int id, IdentityUser user, string note)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie is null)
            return null;

        var item = await _context.WatchlistItem
            .FirstOrDefaultAsync(w => w.MovieId == id && w.User.Id == user.Id);

        if (item is null)
            return null;

        item.PersonalNote = note;

        _context.WatchlistItem.Update(item);

        await _context.SaveChangesAsync();

        return item;
    }
}