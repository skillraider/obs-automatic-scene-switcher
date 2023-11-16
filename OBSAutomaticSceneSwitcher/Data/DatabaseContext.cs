using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OBSAutomaticSceneSwitcher;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=obsAutomaticsSceneSwitcher.db");
    }

    public DbSet<WindowToScene> WindowToScenes { get; set; }

    public DbSet<ConnectionSettings> ConnectionSettings { get; set; }
}