using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWatchlistAPI.Models;

namespace MovieWatchlistAPI.Data;

public class AppDbContext : IdentityDbContext
{

    public DbSet<Movie> Movies { get; set; }
    public DbSet<WatchlistItem> WatchlistItem { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}