using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiniJackpotManager.Data.Entities;
using MiniJackpotManager.Data.Extensions;

namespace MiniJackpotManager.Data;

public class JackpotDbContext : DbContext
{
    public JackpotDbContext(DbContextOptions<JackpotDbContext> options)
        : base(options)
    {
    }

    public DbSet<Jackpot> Jackpots { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SeedJackpots(); // Call seed method from extension
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var updatedEntries = ChangeTracker
            .Entries<Jackpot>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in updatedEntries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
