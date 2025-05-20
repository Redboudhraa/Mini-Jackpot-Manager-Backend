using Microsoft.Extensions.Logging;
using MiniJackpotManager.Data.Entities;
using MiniJackpotManager.Data.Repositories;
using MiniJackpotManager.Services;
using Moq;

namespace MiniJackpotManager.Tests;

public class JackpotServiceTests
{
    private readonly Mock<IJackpotRepository> _mockRepository;
    private readonly Mock<ILogger<JackpotService>> _mockLogger;
    private readonly JackpotService _service;

    public JackpotServiceTests()
    {
        _mockRepository = new Mock<IJackpotRepository>();
        _mockLogger = new Mock<ILogger<JackpotService>>();
        _service = new JackpotService(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task ContributeToJackpot_IncreasesJackpotValue()
    {
        // Arrange
        var jackpotId = 1;
        var initialValue = 500m;
        var contributionAmount = 100m;
        var jackpot = new Jackpot
        {
            Id = jackpotId,
            Name = "Test Jackpot",
            CurrentValue = initialValue,
            SeedValue = 100m,
            HitThreshold = 1000m
        };

        _mockRepository.Setup(r => r.GetJackpotByIdAsync(jackpotId))
            .ReturnsAsync(jackpot);
        _mockRepository.Setup(r => r.UpdateJackpotAsync(It.IsAny<Jackpot>()))
            .ReturnsAsync((Jackpot j) => j);

        // Act
        var contributionResponse = await _service.ContributeToJackpotAsync(jackpotId, contributionAmount);

        // Assert
        Assert.Equal(initialValue + contributionAmount, contributionResponse.CurrentValue);
        Assert.False(contributionResponse.WasReset);
        _mockRepository.Verify(r => r.UpdateJackpotAsync(It.IsAny<Jackpot>()), Times.Once);
    }

    [Fact]
    public async Task ContributeToJackpot_ResetsWhenThresholdReached()
    {
        // Arrange
        var jackpotId = 1;
        var initialValue = 900m;
        var contributionAmount = 200m;
        var seedValue = 100m;
        var jackpot = new Jackpot
        {
            Id = jackpotId,
            Name = "Test Jackpot",
            CurrentValue = initialValue,
            SeedValue = seedValue,
            HitThreshold = 1000m
        };

        _mockRepository.Setup(r => r.GetJackpotByIdAsync(jackpotId))
            .ReturnsAsync(jackpot);
        _mockRepository.Setup(r => r.UpdateJackpotAsync(It.IsAny<Jackpot>()))
            .ReturnsAsync((Jackpot j) => j);

        // Act
        var contributionResponse = await _service.ContributeToJackpotAsync(jackpotId, contributionAmount);

        // Assert
        Assert.Equal(seedValue, contributionResponse.CurrentValue);
        Assert.True(contributionResponse.WasReset);
        _mockRepository.Verify(r => r.UpdateJackpotAsync(It.IsAny<Jackpot>()), Times.Once);
    }

    [Fact]
    public async Task ContributeToJackpot_ThrowsException_WhenJackpotNotFound()
    {
        // Arrange
        var jackpotId = 999;
        var contributionAmount = 100m;

        _mockRepository.Setup(r => r.GetJackpotByIdAsync(jackpotId))
            .ReturnsAsync((Jackpot)null!);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _service.ContributeToJackpotAsync(jackpotId, contributionAmount));

        _mockRepository.Verify(r => r.UpdateJackpotAsync(It.IsAny<Jackpot>()), Times.Never);
    }
}
