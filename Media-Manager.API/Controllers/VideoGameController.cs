using MediaManager.Core.DTOs;
using MediaManager.Core.Interfaces;
using MediaManager.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MediaManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoGameController : ControllerBase
{
    private readonly IVideoGameRepository _repository;

    public VideoGameController(IVideoGameRepository repository)
    {
        _repository = repository;
    }

    // GET: api/videoGames
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGame>>> GetAll()
    {
        var videoGames = await _repository.GetAllAsync();
        return Ok(videoGames);
    }

    // GET: api/videoGames/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGame>> GetById(int id)
    {
        var videoGame = await _repository.GetByIdAsync(id);

        if (videoGame == null)
        {
            return NotFound(new { message = $"Video game with id: {id} not found" });
        }

        return Ok(videoGame);
    }

    // POST: api/videoGames
    [HttpPost]
    public async Task<ActionResult<VideoGame>> Create([FromBody] CreateVideoGameDto videoGameDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var videoGame = new VideoGame
        {
            Title = videoGameDto.Title,
            Description = videoGameDto.Description,
            EstimatedPlayTime = videoGameDto.EstimatedPlayTime,
            UserPlayTime = videoGameDto.UserPlayTime,
            Tags = videoGameDto.Tags,
            CreatedAt = DateTime.UtcNow
        };

        var createdVideoGame = await _repository.CreateAsync(videoGame);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdVideoGame.Id },
            createdVideoGame
        );
    }

    // PUT: api/videoGames/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVideoGameDto updateVideoGameDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var videoGame = new VideoGame
        {
            Title = updateVideoGameDto.Title,
            Description = updateVideoGameDto.Description,
            EstimatedPlayTime = updateVideoGameDto.EstimatedPlayTime,
            UserPlayTime = updateVideoGameDto.UserPlayTime,
            Tags = updateVideoGameDto.Tags,
            UpdatedAt = DateTime.UtcNow
        };

        var updatedVideoGame = await _repository.UpdateAsync(videoGame);
        if (updatedVideoGame == null)
        {
            return NotFound(new { message = $"Video game with id: {id} not found" });
        }

        return Ok(updatedVideoGame);
    }

    // PATCH: api/videoGames/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchVideoGame(int id, [FromBody] JsonPatchDocument<VideoGame> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }

        var videoGame = await _repository.GetByIdAsync(id);
        if (videoGame == null)
        {
            return NotFound(new { message = $"Video game with id: {id} not found" });
        }

        patchDoc.ApplyTo(videoGame);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        videoGame.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(videoGame);
        return Ok(videoGame);
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Video game with id: {id} not found" });
        }
        return NoContent();
    }
}