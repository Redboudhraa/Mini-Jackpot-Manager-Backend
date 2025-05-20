namespace MiniJackpotManager.Models;

/// <summary>
/// Data transfer object for jackpot contribution responses
/// </summary>
public class ContributionResponseDto
{
    /// <summary>
    /// The identifier of the jackpot that received the contribution
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The current value of the jackpot after the contribution
    /// </summary>
    public decimal CurrentValue { get; set; }

    /// <summary>
    /// Indicates whether the jackpot reached its threshold and was reset
    /// </summary>
    public bool WasReset { get; set; }

    /// <summary>
    /// Optional message providing additional details about the contribution result
    /// </summary>
    /// <remarks>
    /// Contains a notification message when the jackpot was reset
    /// </remarks>
    public string? Message { get; set; }
}