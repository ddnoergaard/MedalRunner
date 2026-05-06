using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IDungeonRepository
    {
        Task<IEnumerable<Boss>> GetBossesByDungeonId(int dungeonId);
    }
}
