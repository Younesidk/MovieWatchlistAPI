using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieWatchlistAPI.DTO;
using MovieWatchlistAPI.Models;

namespace MovieWatchlistAPI.Repositories;

public interface IWatchlistItemRepository
{
    public Task<List<WatchlistItem>> GetAllWatchListItems(string userId);

    public Task<WatchlistItem?> AddMovieToWatchList(int id,IdentityUser user);

    public Task<WatchlistItem?> UpdateWatchStatus(int id,string status, IdentityUser user);

    public Task<bool> RemoveFromWatchList(int id, IdentityUser user);

    public Task<WatchlistItem?> UpdatePersonalNote(int id, IdentityUser user,string note);
}