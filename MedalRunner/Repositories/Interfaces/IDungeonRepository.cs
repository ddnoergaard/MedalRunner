using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IDungeonRepository
    {
        Task<List<Dungeon>> GetAllDungeons();
    }
}
