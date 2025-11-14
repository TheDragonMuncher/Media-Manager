using System.Data.Common;
using MediaManager.Core.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



    public DbSet<MediaObject> MediaObjects {get; set;}
    public DbSet<VideoGame> VideoGames {get; set;}
    public DbSet<Video> Videos {get; set;}
    public DbSet<Book> Books { get; set; }
    // public DbSet<Review> Reviews {get; set;}
    // public DbSet<DailyLog> DailyLogs {get; set;}
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.UserName).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.NormalizedEmail).IsRequired();
            entity.Property(u => u.NormalizedUserName).IsRequired();
            entity.Property(u => u.FirstName).IsRequired();
            entity.Property(u => u.LastName).IsRequired();
            entity.Property(u => u.PhoneNumber).IsRequired();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(r => r.Description).IsRequired();
        });

        modelBuilder.Entity<MediaObject>(entity =>
        {
            entity.HasKey(mo => new { mo.Id, mo.Type });

            entity.HasOne(mo => mo.User)
                .WithMany(u => u.MediaObjects)
                .HasForeignKey(mo => mo.UserId);
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

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Title).IsRequired().HasMaxLength(100);
            entity.Property(v => v.Description).IsRequired().HasMaxLength(500);
            entity.Property(v => v.UserWatchTime).IsRequired().HasDefaultValue(0);
            entity.Property(v => v.VideoDuration).IsRequired().HasDefaultValue(0);
            entity.Property(v => v.NumberOfEpisodes).HasDefaultValue(0);

            entity.HasOne(v => v.MediaObject)
                .WithOne(mo => mo.Video)
                .HasForeignKey<Video>(v => v.MediaObjectId)
                .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.AuthorName).IsRequired().HasMaxLength(100);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(100);
            entity.Property(b => b.Summary).IsRequired().HasMaxLength(250);
            entity.Property(b => b.Genre).IsRequired();
            entity.Property(b => b.ISBN).IsRequired();
            entity.Property(b => b.Genre).IsRequired();
            entity.Property(b => b.NumberOfPages).IsRequired().HasDefaultValue(1);

            entity.HasOne(v => v.MediaObject)
                 .WithOne(mo => mo.Book)
                 .HasForeignKey<Book>(b => b.MediaObjectId)
                 .OnDelete(DeleteBehavior.Cascade);
        });
    }
}