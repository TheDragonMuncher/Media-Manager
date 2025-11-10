using MediaManager.Core.DTOs;
using MediaManager.Core.Interfaces;
using MediaManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
namespace MediaManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{

    private readonly IVideoRepository _repository;


    public VideoController(IVideoRepository repository)
    {
        _repository = repository;
    }


    // GET: api/videos

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Video>>> GetAll()
    {
        var videos = await _repository.GetAllAsync();
        return Ok(videos);
    }



    // GET: api/videoGames/{id}

    [HttpGet("{id}")]

    public async Task<ActionResult<Video>> GetById(int id)
    {
        var video = await _repository.GetByIdAsync(id);

        if (video == null)
        {
            return NotFound(new { message = $"Video with id: {id} not found" });
        }

        return Ok(video);
    }

    //POST: api/videos
    [HttpPost]
    public async Task<ActionResult<Video>> Create([FromBody] CreateVideoDto videoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var video = new Video
        {
            Title = videoDto.Title,
            Description = videoDto.Description,
            UserWatchTime = videoDto.UserWatchTime,
            VideoDuration = videoDto.VideoDuration,
            NumberOfEpisodes = videoDto.NumberOfEpisodes,
            Tags = videoDto.Tags,
            CreatedAt = DateTime.UtcNow
        };

        var createdVideo = await _repository.CreateAsync(video);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdVideo.Id },
            createdVideo

        );
    }



    //Put: api/video/{id}

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, [FromBody] UpdateVideoDto updateVideoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var video = new Video
        {
            Title = updateVideoDto.Title,
            Description = updateVideoDto.Description,
            UserWatchTime = updateVideoDto.UserWatchTime,
            VideoDuration = updateVideoDto.VideoDuration,
            NumberOfEpisodes = updateVideoDto.NumberOfEpisodes,
            Tags = updateVideoDto.Tags,
            CreatedAt = DateTime.UtcNow
        };

        var updatedVideo = await _repository.UpdateAsync(video);
        if (updatedVideo == null)
        {
            return NotFound(new { message = $"Video  with id: {id} not found" });
        }

        return Ok(updatedVideo);
    }


    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchVideo(int id, [FromBody] JsonPatchDocument<Video> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }

        var video = await _repository.GetByIdAsync(id);
        if (video == null)
        {
            return NotFound(new { message = $"Video with id: {id} not found" });
        }

        patchDoc.ApplyTo(video);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        video.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(video);
        return Ok(video);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Video with id: {id} not found" });
        }
        return NoContent();
    }



}