using System;
using Media_Manager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Media_Manager.Infrastructure.Persistence;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Content).IsRequired().HasMaxLength(1500);
        builder.Property(e => e.Rating).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt);
        builder.HasOne(e => e.MediaObject)
        .WithOne(e => e.Review)
        .HasForeignKey<Review>(e => e.MediaObjectId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}


