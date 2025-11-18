using System;
using MediaManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Media_Manager.Infrastructure.Persistence;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.AuthorName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Summary).IsRequired().HasMaxLength(250);
        builder.Property(e => e.Genre).IsRequired();
        builder.Property(e => e.ISBN).IsRequired();
        builder.Property(e => e.Genre).IsRequired();
        builder.Property(e => e.NumberOfPages).IsRequired().HasDefaultValue(1);
        builder.Property(e => e.PublicationYear);
        builder.Property(e => e.CoverImageURL);
        builder.HasOne(e => e.MediaObject)
             .WithOne(e => e.Book)
             .HasForeignKey<Book>(e => e.MediaObjectId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}