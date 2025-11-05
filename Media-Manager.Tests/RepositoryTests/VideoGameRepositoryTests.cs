using MediaManager.Core.Models;
using MediaManager.Infrastructure.Data;
using MediaManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediaManager.Tests.RepositoryTests;

public class VideoGameRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly VideoGameRepository _repository;

    public VideoGameRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new VideoGameRepository(_context);

        SeedTestData();
    }

    private void SeedTestData()
    {
        var videoGames = new[]
        {
            new VideoGame
            {
                Id = 0,
                Title = "Skyrim",
                Description = "So, you've finally awoken.",
                EstimatedPlayTime = 60.0,
                UserPlayTime = 10.4,
                MediaObjectId = 0,
                CreatedAt = DateTime.UtcNow
            },
            new VideoGame
            {
                Id = 1,
                Title = "Factorio",
                Description = "Damn biters!",
                EstimatedPlayTime = double.MaxValue,
                UserPlayTime = 524.3,
                MediaObjectId = 1,
                CreatedAt = DateTime.UtcNow
            }
        };
        var mediaObjects = new[]
        {
            new MediaObject
            {
                Id = 0
            },
            new MediaObject
            {
                Id = 1
            }
        };

        videoGames[0].MediaObject = mediaObjects[0];
        videoGames[1].MediaObject = mediaObjects[1];
        mediaObjects[0].VideoGame = videoGames[0];
        mediaObjects[1].VideoGame = videoGames[1];

        _context.VideoGames.AddRange(videoGames);
        _context.MediaObjects.AddRange(mediaObjects);
        _context.SaveChanges();
    }

    [Fact]
    public void GetAllAsync_ReturnsAllVideoGames()
    {

    }

    [Fact]
    public void GetByIdAsync_ValidId_ReturnsVideoGame()
    {

    }

    [Fact]
    public void GetByIdAsync_InvalidId_ReturnsNull()
    {

    }

    [Fact]
    public void CreateAsync_ValidVideoGame_AddsToDatabase()
    {

    }

    [Fact]
    public void CreateAsync_InvalidVideoGame_ReturnsNull()
    {

    }

    [Fact]
    public void UpdateAsync_ValidVideoGame_UpdatesInDatabase()
    {

    }

    [Fact]
    public void UpdateAsync_InvalidVideoGame_ReturnsNull()
    {

    }

    [Fact]
    public void DeleteAsync_ValidId_RemovesFromDatabase()
    {

    }

    [Fact]
    public void DeleteAsync_InvalidId_ReturnsFalse()
    {
        
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}