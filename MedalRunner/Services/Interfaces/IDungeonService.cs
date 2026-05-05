using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IDungeonService
    {
        Task<List<Dungeon>> GetAllDungeons();
    }
}
