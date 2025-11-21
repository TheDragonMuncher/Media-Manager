using MediaManager.Core.Interfaces;
using MediaManager.Core.Models;
using MediaManager.Infrastructure.Data;

namespace MediaManager.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Review> CreateAsync(Review review)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Review>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Review> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Review> UpdateAsync(Review review)
    {
        throw new NotImplementedException();
    }
}