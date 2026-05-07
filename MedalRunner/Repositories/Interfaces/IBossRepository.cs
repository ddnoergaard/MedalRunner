using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IBossRepository
    {
        Task AddAsync(Boss boss, int dungeonId);
        Task<IEnumerable<Boss>> GetAllBossesAsync();
        Task DeleteAsync(int id);
        Task UpdateImageUrlAsync(int bossId, string imageUrl);
        Task<Boss> GetBossByIdAsync(int id);
        Task<Boss> GetBossByNameAsync(string name);
    }
}
