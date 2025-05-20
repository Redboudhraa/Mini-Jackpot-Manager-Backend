using System.ComponentModel.DataAnnotations;

namespace MiniJackpotManager.Models;

/// <summary>
/// Data transfer object for jackpot contribution requests
/// </summary>
public class ContributionRequestDto
{
    /// <summary>
    /// The monetary amount to contribute to the jackpot
    /// </summary>
    /// <remarks>
    /// Must be greater than zero
    /// </remarks>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
    public decimal Amount { get; set; }
}
