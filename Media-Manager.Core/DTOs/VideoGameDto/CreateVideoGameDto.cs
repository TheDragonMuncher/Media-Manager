using System.ComponentModel.DataAnnotations;
using MediaManager.Core.Enums;

namespace MediaManager.Core.DTOs;

public class CreateVideoGameDto
{
    [Required(ErrorMessage = "The game must have a title")]
    [MaxLength(100, ErrorMessage = "The max length of the title is 100 characters")]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500, ErrorMessage = "The max length of the description is 500 characters")]
    public string Description { get; set; } = string.Empty;
    [Range(0, int.MaxValue, ErrorMessage = "The user's play time must be at least 0")]
    public int UserPlayTime { get; set; } = 0;
    [Range(0, int.MaxValue, ErrorMessage = "The estimated play time must be at least 0")]
    public int EstimatedPlayTime { get; set; } = 0;
    [Required(ErrorMessage = "There must be at least 1 tag")]
    public ICollection<VideoGameTagEnum> Tags { get; set; }
}