using MediaManager.Core.Interfaces;
using MediaManager.Core.Models;
using MediaManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Infrastructure.Repositories;

public class VideoGameRepository : IVideoGameRepository
{
    private readonly ApplicationDbContext _context;
    public VideoGameRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<VideoGame> CreateAsync(VideoGame game)
    {
        game.CreatedAt = DateTime.UtcNow;

        // var mediaObject = new MediaObject
        // {
        //     Id = game.Id,
        //     Type = Core.Enums.MediaObjectTypeEnum.VideoGame,
        //     VideoGame = game
        // };

        // _context.VideoGames.Add(game);
        // _context.MediaObjects.Add(mediaObject);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var currentGame = await _context.VideoGames.FindAsync(id);
        if (currentGame == null)
        {
            return false;
        }

        _context.VideoGames.Remove(currentGame);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<VideoGame>> GetAllAsync()
    {
        return await _context.VideoGames.ToListAsync();
    }

    public async Task<VideoGame?> GetByIdAsync(int id)
    {
        return await _context.VideoGames.FindAsync(id);
    }

    public async Task<VideoGame> UpdateAsync(VideoGame game)
    {
        var currentGame = await _context.VideoGames.FindAsync(game.Id);
        if (currentGame == null)
        {
            return null;
        }

        currentGame.Title = game.Title;
        currentGame.Description = game.Description;
        currentGame.EstimatedPlayTime = game.EstimatedPlayTime;
        currentGame.UserPlayTime = game.UserPlayTime;
        currentGame.Tags = game.Tags;
        currentGame.UpdatedAt = DateTime.UtcNow;

        _context.VideoGames.Update(currentGame);
        await _context.SaveChangesAsync();
        return currentGame;
    }
}