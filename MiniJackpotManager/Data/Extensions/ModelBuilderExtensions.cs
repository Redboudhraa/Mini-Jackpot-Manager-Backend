using Microsoft.EntityFrameworkCore;
using MiniJackpotManager.Data.Entities;

namespace MiniJackpotManager.Data.Extensions;
public static class JackpotSeedConstants
{
    public const int MiniJackpotId = 1;
    public const int MajorJackpotId = 2;
    public const int GrandJackpotId = 3;
}
public static class ModelBuilderExtensions
{
    public static void SeedJackpots(this ModelBuilder modelBuilder)
    {
        var now = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Jackpot>().HasData(
            new Jackpot
            {
                Id = JackpotSeedConstants.MiniJackpotId,
                Name = "Mini Jackpot",
                CurrentValue = 100.00m,
                SeedValue = 100.00m,
                HitThreshold = 1000.00m,
                CreatedAt = now,
                UpdatedAt = now
            },
            new Jackpot
            {
                Id = JackpotSeedConstants.MajorJackpotId,
                Name = "Major Jackpot",
                CurrentValue = 1000.00m,
                SeedValue = 1000.00m,
                HitThreshold = 10000.00m,
                CreatedAt = now,
                UpdatedAt = now
            },
            new Jackpot
            {
                Id = JackpotSeedConstants.GrandJackpotId,
                Name = "Grand Jackpot",
                CurrentValue = 10000.00m,
                SeedValue = 10000.00m,
                HitThreshold = 100000.00m,
                CreatedAt = now,
                UpdatedAt = now
            }
        );
    }
}