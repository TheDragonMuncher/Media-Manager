using System.ComponentModel.DataAnnotations;
using MediaManager.Core.Enums;

namespace MediaManager.Core.Models;

public class VideoGame
{
    // Properties

    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "The game must have a title")]
    [MaxLength(100, ErrorMessage = "The max length of the title is 100 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "The game requires a description")]
    [MaxLength(500, ErrorMessage = "The max length of the description is 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "The user's play time must be at least 0")]
    public double UserPlayTime { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = "The estimated play time must be at least 0")]
    public double EstimatedPlayTime { get; set; } = 0;

    [Required(ErrorMessage = "There must be at least 1 tag")]
    public ICollection<VideoGameTagEnum>? Tags { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }


    // Relations

    public int MediaObjectId { get; set; }
    public MediaObject? MediaObject { get; set; }

}