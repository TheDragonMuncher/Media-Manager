using System.Data.Common;
using MediaManager.Core.Models;
using MediaManager.Infrastructure.Configuration;
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
    public DbSet<Review> Reviews { get; set; }
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

        new VideoGameConfiguration().Configure(modelBuilder.Entity<VideoGame>());        
        new VideoConfiguration().Configure(modelBuilder.Entity<Video>());
        new BookConfiguration().Configure(modelBuilder.Entity<Book>());
        new ReviewConfiguration().Configure(modelBuilder.Entity<Review>());
    }
}