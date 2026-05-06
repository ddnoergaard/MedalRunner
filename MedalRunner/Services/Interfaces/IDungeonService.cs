using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IDungeonService
    {
        Task AddDungeon(Dungeon dungeon);
        Task UpdateDungeon(Dungeon dungeon);
        Task DeleteDungeon(int id);
    }
}
