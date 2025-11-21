using MediaManager.Core.Models;

namespace MediaManager.Core.Interfaces;

public interface IReviewRepository
{
    Task<ICollection<Review>> GetAllAsync();
    Task<Review> GetByIdAsync(int id);
    Task<Review> CreateAsync(Review review);
    Task<bool> DeleteAsync(int id);
    Task<Review> UpdateAsync(Review review);
}