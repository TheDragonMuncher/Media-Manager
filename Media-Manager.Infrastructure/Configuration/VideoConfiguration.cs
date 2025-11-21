using System;
using MediaManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Configuration;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.HasKey(v => v.Id);
            builder.Property(v => v.Title).IsRequired().HasMaxLength(100);
            builder.Property(v => v.Description).IsRequired().HasMaxLength(500);
            builder.Property(v => v.UserWatchTime).IsRequired().HasDefaultValue(0);
            builder.Property(v => v.VideoDuration).IsRequired().HasDefaultValue(0);
            builder.Property(v => v.NumberOfEpisodes).HasDefaultValue(0);
            builder.HasOne(v => v.MediaObject)
                .WithOne(mo => mo.Video)
                .HasForeignKey<Video>(v => v.MediaObjectId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
