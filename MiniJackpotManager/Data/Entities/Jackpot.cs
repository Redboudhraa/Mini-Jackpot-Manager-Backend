using System.ComponentModel.DataAnnotations;

namespace MiniJackpotManager.Data.Entities;

public class Jackpot
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, Range(0, double.MaxValue)]
    public decimal CurrentValue { get; set; }

    [Required, Range(0, double.MaxValue)]
    public decimal SeedValue { get; set; }

    [Required, Range(0, double.MaxValue)]
    public decimal HitThreshold { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
