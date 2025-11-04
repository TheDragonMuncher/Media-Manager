using MediaManager.Core.Interfaces;
using MediaManager.Core.Models;
using MediaManager.Infrastructure.Data;

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

        var mediaObject = new MediaObject();

        _context.VideoGames.Add(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<VideoGame>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<VideoGame> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<VideoGame> UpdateAsync(VideoGame game)
    {
        throw new NotImplementedException();
    }
}