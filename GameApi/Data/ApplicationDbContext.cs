using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Data;

public class ApplicationDbContext: DbContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<GameAuthToken> GameAuthTokens => Set<GameAuthToken>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Score> Scores => Set<Score>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=application.db");
    }

    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }
}