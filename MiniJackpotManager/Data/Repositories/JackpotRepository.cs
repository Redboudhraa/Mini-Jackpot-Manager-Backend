using Microsoft.EntityFrameworkCore;
using MiniJackpotManager.Data.Entities;

namespace MiniJackpotManager.Data.Repositories;

public class JackpotRepository : IJackpotRepository
{
    private readonly JackpotDbContext _context;

    public JackpotRepository(JackpotDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Jackpot>> GetAllJackpotsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Jackpots
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<Jackpot?> GetJackpotByIdAsync(int id)
    {
        return await _context.Jackpots.FindAsync(id);
    }

    public async Task<bool> JackpotExistsAsync(int id)
    {
        return await _context.Jackpots.AnyAsync(j => j.Id == id);
    }

    public async Task<Jackpot> UpdateJackpotAsync(Jackpot jackpot)
    {
        _context.Jackpots.Update(jackpot);
        await SaveChangesAsync();
        return jackpot;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}