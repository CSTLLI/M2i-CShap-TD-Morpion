using Microsoft.EntityFrameworkCore;

namespace Morpion;

public class MorpionDbContext : DbContext
{
    public DbSet<GameRecord> GameRecords { get; set; }
    public DbSet<GameState> GameStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Port=5432;Database=morpion;Username=morpion;Password=morpion");
    }
}
