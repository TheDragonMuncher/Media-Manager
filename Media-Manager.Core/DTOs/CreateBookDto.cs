using System;
using System.ComponentModel.DataAnnotations;

namespace Media_Manager.Core.DTOs;

public class CreateBookDto
{
    [Required(ErrorMessage = "The Book Must have an author")]
    [MaxLength(100, ErrorMessage = "Character amount exceeds the maximum amount")]
    public string AuthorName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100, ErrorMessage = "Character amount exceeds the maximum amount")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(250, ErrorMessage = "Character amount exceeds the maximum amount")]
    public string Summary { get; set; } = string.Empty;
    [Required]
    public string Genre { get; set; } = string.Empty;
    [Required]
    public string ISBN { get; set; } = string.Empty;
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The Book Must have a minimum of atleast 1 page")]
    public int NumberOfPages { get; set; }
    [Required]
    public int PublicationYear { get; set; }
    public string CoverImageURL { get; set; } = string.Empty;
}
