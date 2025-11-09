using System.Threading.Tasks;
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
                Id = 0,
                Type = Core.Enums.MediaObjectTypeEnum.VideoGame
            },
            new MediaObject
            {
                Id = 1,
                Type = Core.Enums.MediaObjectTypeEnum.VideoGame
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
    public async Task GetAllAsync_ReturnsAllVideoGames()
    {
        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsVideoGame()
    {
        // Act
        var result = await _repository.GetByIdAsync(0);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Skyrim", result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(3);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidVideoGame_AddsToDatabase()
    {
        // Arrange
        var game = new VideoGame
        {
            Id = 2,
            Title = "Minecraft",
            Description = "Creeper, Awww man. ",
            EstimatedPlayTime = double.MaxValue,
            UserPlayTime = 2341.7,
            MediaObjectId = 2
        };

        // Act
        await _repository.CreateAsync(game);

        var testGame = await _repository.GetByIdAsync(2);
        var testMediaObject = _context.MediaObjects.Find(2);

        // Assert
        Assert.NotNull(testGame);
        Assert.NotNull(testMediaObject);
        Assert.Equal("Minecraft", testGame.Title);
        Assert.Equal(2, testMediaObject.Id);
    }

    [Fact]
    public async Task UpdateAsync_ValidVideoGame_UpdatesInDatabase()
    {
        // Arrange 
        var game = new VideoGame
        {
            Id = 1,
            Title = "Mario Galaxy",
            Description = "Wahooo!",
            EstimatedPlayTime = double.MaxValue,
            UserPlayTime = 2
        };

        // Act
        await _repository.UpdateAsync(game);
        var testGame = await _repository.GetByIdAsync(game.Id);

        // Assert
        Assert.NotNull(testGame);
        Assert.Equal(game.Title, testGame.Title);
    }

    [Fact]
    public async Task UpdateAsync_InvalidVideoGame_ReturnsNull()
    {
        // Arrange 
        var game = new VideoGame
        {
            Id = 3,
            Title = "Mario Galaxy",
            Description = "Wahooo!",
            EstimatedPlayTime = double.MaxValue,
            UserPlayTime = 2
        };

        // Act
        await _repository.UpdateAsync(game);
        var testGame = await _repository.GetByIdAsync(game.Id);

        // Assert
        Assert.Null(testGame);
    }

    [Fact]
    public async Task DeleteAsync_ValidId_RemovesFromDatabase()
    {
        // Act
        var result = await _repository.DeleteAsync(0);
        var game = _context.VideoGames.Find(0);
        var mediaObject = _context.MediaObjects.Find(0);

        // Assert
        Assert.True(result);
        Assert.Null(game);
        Assert.Null(mediaObject);
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ReturnsFalse()
    {
        // Act
        var result = await _repository.DeleteAsync(4);

        // Assert
        Assert.False(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}