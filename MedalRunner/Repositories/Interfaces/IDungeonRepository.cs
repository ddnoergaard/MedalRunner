using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IDungeonRepository
    {
        Task<List<Dungeon>> GetAllDungeonsAsync();
        Task AddDungeonAsync(Dungeon dungeon);
        Task UpdateDungeonAsync(Dungeon dungeon);
        Task DeleteDungeonAsync(int id);
        Task<IEnumerable<Boss>> GetBossesByDungeonIdAsync(int dungeonId);
    }
}
