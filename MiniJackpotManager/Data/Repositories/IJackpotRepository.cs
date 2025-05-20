using MiniJackpotManager.Data.Entities;

namespace MiniJackpotManager.Data.Repositories;

public interface IJackpotRepository
{
    Task<IEnumerable<Jackpot>> GetAllJackpotsAsync(CancellationToken cancellationToken = default);
    Task<Jackpot?> GetJackpotByIdAsync(int id);
    Task<bool> JackpotExistsAsync(int id);
    Task<Jackpot> UpdateJackpotAsync(Jackpot jackpot);
    Task SaveChangesAsync();
}
