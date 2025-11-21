using System;
using MediaManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaManager.Infrastructure.Configuration;

public class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
{
    public void Configure(EntityTypeBuilder<VideoGame> builder)
    {
        builder.HasKey(vg => vg.Id);
            builder.Property(vg => vg.Title).IsRequired().HasMaxLength(100);
            builder.Property(vg => vg.Description).IsRequired().HasMaxLength(500);
            builder.Property(vg => vg.UserPlayTime).HasDefaultValue(0);
            builder.Property(vg => vg.EstimatedPlayTime).HasDefaultValue(0);
            builder.HasOne(vg => vg.MediaObject)
                .WithOne(mo => mo.VideoGame)
                .HasForeignKey<VideoGame>(vg => vg.MediaObjectId)
                .OnDelete(DeleteBehavior.Cascade);   
    }
}
