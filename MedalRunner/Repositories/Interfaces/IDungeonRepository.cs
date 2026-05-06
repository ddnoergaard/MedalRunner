using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IDungeonRepository
    {
        Task AddDungeon(Dungeon dungeon);
        Task UpdateDungeon(Dungeon dungeon);
        Task DeleteDungeon(int id);
    }
}
