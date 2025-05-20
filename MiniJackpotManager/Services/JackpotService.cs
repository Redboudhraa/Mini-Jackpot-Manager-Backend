using MiniJackpotManager.Data.Repositories;
using MiniJackpotManager.Models;

namespace MiniJackpotManager.Services;

/// <summary>
/// Implementation of the jackpot service that manages jackpot operations
/// </summary>
public class JackpotService : IJackpotService
{
    private readonly IJackpotRepository _repository;
    private readonly ILogger<JackpotService> _logger;

    /// <summary>
    /// Initializes a new instance of the JackpotService class
    /// </summary>
    /// <param name="repository">The jackpot repository for data access</param>
    /// <param name="logger">The logger for service operations</param>
    public JackpotService(IJackpotRepository repository, ILogger<JackpotService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<JackpotResponseDto>> GetAllJackpotsAsync()
    {
        var jackpots = await _repository.GetAllJackpotsAsync();

        return jackpots.Select(j => new JackpotResponseDto
        {
            Id = j.Id,
            Name = j.Name,
            CurrentValue = j.CurrentValue,
            SeedValue = j.SeedValue,
            HitThreshold = j.HitThreshold
        });
    }

    /// <inheritdoc />
    public async Task<ContributionResponseDto> ContributeToJackpotAsync(int jackpotId, decimal amount)
    {
        var jackpot = await _repository.GetJackpotByIdAsync(jackpotId);

        if (jackpot == null)
        {
            throw new KeyNotFoundException($"Jackpot with ID {jackpotId} not found");
        }

        // Add the contribution amount
        jackpot.CurrentValue += amount;

        bool wasReset = false;

        // Check if the jackpot hit the threshold
        if (jackpot.CurrentValue >= jackpot.HitThreshold)
        {
            _logger.LogInformation($"Jackpot '{jackpot.Name}' reached threshold of {jackpot.HitThreshold:C}. Resetting to seed value of {jackpot.SeedValue:C}");
            jackpot.CurrentValue = jackpot.SeedValue;
            wasReset = true;
        }

        // Update the jackpot
        await _repository.UpdateJackpotAsync(jackpot);


        return new ContributionResponseDto
        {
            Id = jackpot.Id,
            CurrentValue = jackpot.CurrentValue,
            WasReset = wasReset,
            Message = wasReset ? $"Jackpot '{jackpot.Name}' reached threshold and was reset to seed value" : null
        };
    }
}