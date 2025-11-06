using MediaManager.Core.Models;

namespace MediaManager.Core.Interfaces;

public interface IVideoRepository
{
    Task<ICollection<Video>> GetAllAsync();
    Task<Video> GetByIdAsync(int id);
    Task<Video> CreateAsync(Video video);
    Task<bool> DeleteAsync(int id);
    Task<Video> UpdateAsync(Video video);
}