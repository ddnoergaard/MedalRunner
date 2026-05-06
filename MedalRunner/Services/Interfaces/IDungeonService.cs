using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IDungeonService
    {
        Task<IEnumerable<Boss>> GetBossesAsync(int dungeonId);
    }
}
