namespace MiniJackpotManager.Models;

/// <summary>
/// Data transfer object for jackpot information
/// </summary>
public class JackpotResponseDto
{
    /// <summary>
    /// Unique identifier for the jackpot
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Display name of the jackpot
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Current monetary value of the jackpot
    /// </summary>
    public decimal CurrentValue { get; set; }

    /// <summary>
    /// Initial value used when resetting the jackpot
    /// </summary>
    public decimal SeedValue { get; set; }

    /// <summary>
    /// Value at which the jackpot triggers and resets to the seed value
    /// </summary>
    public decimal HitThreshold { get; set; }
}