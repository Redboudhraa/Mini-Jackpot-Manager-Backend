using MiniJackpotManager.Models;

namespace MiniJackpotManager.Services;

/// <summary>
/// Service interface for jackpot operations
/// </summary>
public interface IJackpotService
{
    /// <summary>
    /// Retrieves all jackpots from the repository and maps them to response DTOs
    /// </summary>
    /// <returns>A collection of jackpot response DTOs</returns>
    Task<IEnumerable<JackpotResponseDto>> GetAllJackpotsAsync();

    /// <summary>
    /// Adds a contribution to a specific jackpot and checks if the threshold has been reached
    /// </summary>
    /// <param name="jackpotId">The ID of the jackpot to contribute to</param>
    /// <param name="amount">The amount to contribute</param>
    /// <returns>A response containing updated jackpot information and status</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the specified jackpot ID is not found</exception>
    Task<ContributionResponseDto> ContributeToJackpotAsync(int jackpotId, decimal amount);
}
