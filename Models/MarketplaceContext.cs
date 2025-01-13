using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models;

public class MarketplaceContext : DbContext
{
    public MarketplaceContext(DbContextOptions<MarketplaceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Заполняем данными
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem { Id = 1, Name = "Learn EF Core", IsComplete = false },
            new TodoItem { Id = 2, Name = "Write migrations", IsComplete = false },
            new TodoItem { Id = 3, Name = "Deploy to production", IsComplete = false }
        );
    }

    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<Private> Privates { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<PrivateTag> PrivateTags { get; set; }
    public DbSet<Subway> Subways { get; set; }
    public DbSet<Tag> Tags { get; set; }
}