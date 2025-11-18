using System;
using System.ComponentModel.DataAnnotations;
using MediaManager.Core.Models;

namespace MediaManager.Core.Models;

public class Book
{
    public int Id { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    [Range(1, int.MaxValue, ErrorMessage = "The Book Must have a minimum of atleast 1 page")]
    public int NumberOfPages { get; set; }
    [Required]
    public int PublicationYear { get; set; }
    public string CoverImageURL { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    //Relations
    public int MediaObjectId { get; set; }
    public MediaObject MediaObject { get; set; }

}
