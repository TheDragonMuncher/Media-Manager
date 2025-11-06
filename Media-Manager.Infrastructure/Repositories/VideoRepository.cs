using MediaManager.Core.Interfaces;
using MediaManager.Core.Models;
using MediaManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace MediaManager.Infrastructure.Repositories;


public class VideoRepository : IVideoRepository
{

    private readonly ApplicationDbContext _context;

    public VideoRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Video> CreateAsync(Video video)
    {
        video.CreatedAt = DateTime.UtcNow;

        var mediaObject = new MediaObject
        {
            Id = video.Id,
            Type = Core.Enums.MediaObjectTypeEnum.Video,
            Video = video
        };

        _context.Videos.Add(video);
        _context.MediaObjects.Add(mediaObject);
        await _context.SaveChangesAsync();
        return video;

    }

    public async Task<bool> DeleteAsync(int id)
    {
        var currentVideo = await _context.Videos.FindAsync(id);
        if (currentVideo == null)
        {
            return false;
        }

        _context.Videos.Remove(currentVideo);

        await _context.SaveChangesAsync();

        return true;

    }

    public async Task<ICollection<Video>> GetAllAsync()
    {
        return await _context.Videos.ToListAsync(); 
    }

    public async Task<Video?> GetByIdAsync(int id)
    {
        return await _context.Videos.FindAsync(id);
    }

    public async Task<Video> UpdateAsync(Video video)
    {
        var currentVideo = await _context.Videos.FindAsync(video.Id);

        if (currentVideo == null)
        {
            return null;
        }

        currentVideo.Title = video.Title;
        currentVideo.Description = video.Description;
        currentVideo.UserWatchTime = video.UserWatchTime;
        currentVideo.VideoDuration = video.VideoDuration;
        currentVideo.NumberOfEpisodes = video.NumberOfEpisodes;
        currentVideo.Tags = video.Tags;
        currentVideo.UpdatedAt = DateTime.UtcNow;

        _context.Videos.Update(currentVideo);

        await _context.SaveChangesAsync();

        return currentVideo; 

    }
}