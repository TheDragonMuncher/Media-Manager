using System.ComponentModel.DataAnnotations;
using MediaManager.Core.Enums;


namespace MediaManager.Core.DTOs;

public class UpdateVideoDto
{
        // Properties

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "The video must have a title")]
        [MaxLength(100, ErrorMessage = "The max length of the title is 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "The video requires a description")]
        [MaxLength(500, ErrorMessage = "The max length of the description is 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "The user's watch time must be at least 0")]
        public int UserWatchTime { get; set; } = 0; // minutes

        [Range(0, int.MaxValue, ErrorMessage = "The video duration must be at least 0")]
        public int VideoDuration { get; set; } = 0; // minutes


        [Range(0, int.MaxValue, ErrorMessage = "There must be atleast 1 episode")]
         public int NumberOfEpisodes { get; set; } = 0; // minutes


        [Required(ErrorMessage = "There must be at least 1 tag")]
        public ICollection<VideoTagEnum> Tags { get; set; } = new List<VideoTagEnum>();
    
}