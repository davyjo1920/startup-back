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

        modelBuilder.Entity<PrivateTag>()
            .HasKey(pc => new { pc.PrivateId, pc.TagId });

        modelBuilder.Entity<PrivateTag>()
            .HasOne(pc => pc.Private)
            .WithMany(p => p.Tags)
            .HasForeignKey(pc => pc.PrivateId);

        modelBuilder.Entity<PrivateTag>()
            .HasOne(pc => pc.Tag)
            .WithMany(c => c.Privates)
            .HasForeignKey(pc => pc.TagId);

        modelBuilder.Entity<Photo>()
            .HasOne(pc => pc.Private)
            .WithMany(p => p.Photos)
            .HasForeignKey(pc => pc.PrivateId);

        modelBuilder.Entity<PrivateSubway>()
            .HasKey(pc => new { pc.PrivateId, pc.SubwayId });

        modelBuilder.Entity<PrivateSubway>()
            .HasOne(pc => pc.Private)
            .WithMany(p => p.Subways)
            .HasForeignKey(pc => pc.PrivateId);

        modelBuilder.Entity<PrivateSubway>()
            .HasOne(pc => pc.Subway)
            .WithMany(c => c.Privates)
            .HasForeignKey(pc => pc.SubwayId);
    }

    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<Private> Privates { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<PrivateTag> PrivateTags { get; set; }
    public DbSet<Subway> Subways { get; set; }
    public DbSet<Tag> Tags { get; set; }
}