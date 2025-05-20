using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniJackpotManager.Data;
using MiniJackpotManager.Data.Entities;
using MiniJackpotManager.Data.Repositories;
using MiniJackpotManager.Services;
using Moq;

namespace MiniJackpotManager.Tests;

public class JackpotIntegrationTests
{
    private readonly DbContextOptions<JackpotDbContext> _dbOptions;
    private readonly Mock<ILogger<JackpotService>> _mockLogger;

    public JackpotIntegrationTests()
    {
        _dbOptions = new DbContextOptionsBuilder<JackpotDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _mockLogger = new Mock<ILogger<JackpotService>>();

        // Seed test data
        using var context = new JackpotDbContext(_dbOptions);
        context.Jackpots.Add(new Jackpot
        {
            Id = 1,
            Name = "Test Jackpot",
            CurrentValue = 500m,
            SeedValue = 100m,
            HitThreshold = 1000m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });
        context.SaveChanges();
    }

    [Fact]
    public async Task GetAllJackpots_ReturnsAllJackpots()
    {
        // Arrange
        using var context = new JackpotDbContext(_dbOptions);
        var repository = new JackpotRepository(context);
        var service = new JackpotService(repository, _mockLogger.Object);

        // Act
        var jackpots = await service.GetAllJackpotsAsync();

        // Assert
        var jackpotsList = jackpots.ToList();
        Assert.Single(jackpotsList);
        Assert.Equal("Test Jackpot", jackpotsList[0].Name);
    }

    [Fact]
    public async Task ContributeToJackpot_UpdatesJackpotValue()
    {
        // Arrange
        using var context = new JackpotDbContext(_dbOptions);
        var repository = new JackpotRepository(context);
        var service = new JackpotService(repository, _mockLogger.Object);

        var contributionAmount = 200m;

        // Act
        var contributionResponse = await service.ContributeToJackpotAsync(1, contributionAmount);

        // Assert
        Assert.Equal(700m, contributionResponse.CurrentValue);
        Assert.False(contributionResponse.WasReset);

        // Verify the database was updated
        var updatedJackpot = await context.Jackpots.FindAsync(1);
        Assert.Equal(700m, updatedJackpot!.CurrentValue);
    }

    [Fact]
    public async Task ContributeToJackpot_ResetsToSeedValue_WhenThresholdReached()
    {
        // Arrange
        using var context = new JackpotDbContext(_dbOptions);
        var repository = new JackpotRepository(context);
        var service = new JackpotService(repository, _mockLogger.Object);

        var contributionAmount = 600m;

        // Act
        var contributionResponse = await service.ContributeToJackpotAsync(1, contributionAmount);

        // Assert
        Assert.Equal(100m, contributionResponse.CurrentValue);
        Assert.True(contributionResponse.WasReset);

        // Verify the database was updated
        var updatedJackpot = await context.Jackpots.FindAsync(1);
        Assert.Equal(100m, updatedJackpot!.CurrentValue);
    }
}