using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IBossService
    {
        Task AddAsync(Boss boss, int dungeonId);
        Task<IEnumerable<Boss>> GetBossesAsync();
        Task DeleteAsync(int id);
        Task UpdateImageUrl(int bossId, string imageUrl);
        Task<Boss> GetBossById(int id);
        Task<Boss> GetBossByNameAsync(string name);
        Task<IEnumerable<Boss>> GetBossesByItemId(int id);
    }
}
