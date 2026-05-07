using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IDungeonRepository
    {
        Task AddDungeonAsync(Dungeon dungeon);
        Task UpdateDungeonAsync(Dungeon dungeon);
        Task DeleteDungeonAsync(int id);
        Task<List<Dungeon>> GetAllDungeonsAsync();
        Task<IEnumerable<Boss>> GetBossesByDungeonIdAsync(int dungeonId);
    }
}
