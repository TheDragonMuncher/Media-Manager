using System.Data.Common;
using MediaManager.Core.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<MediaObject> MediaObjects;
    public DbSet<VideoGame> VideoGames;
    // public DbSet<Video> Videos;
    // public DbSet<Book> Books;
    // public DbSet<Review> Reviews;
    // public DbSet<DailyLog> DailyLogs;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MediaObject>(entity =>
        {
            entity.HasKey(mo => new { mo.Id, mo.Type });
        });

        modelBuilder.Entity<VideoGame>(entity =>
        {
            entity.HasKey(vg => vg.Id);
            entity.Property(vg => vg.Title).IsRequired().HasMaxLength(100);
            entity.Property(vg => vg.Description).IsRequired().HasMaxLength(500);
            entity.Property(vg => vg.UserPlayTime).HasDefaultValue(0);
            entity.Property(vg => vg.EstimatedPlayTime).HasDefaultValue(0);

            entity.HasOne(vg => vg.MediaObject)
                .WithOne(mo => mo.VideoGame)
                .HasForeignKey<VideoGame>(vg => vg.MediaObjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // modelBuilder.Entity<Video>(entity =>
        // {

        // });

        // modelBuilder.Entity<Book>(entity =>
        // {

        // });
    }
}