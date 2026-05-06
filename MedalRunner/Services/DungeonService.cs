using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Services.Interfaces;

public class DungeonService : IDungeonService
{
    private readonly IDungeonRepository _dungeonRepository;

    public DungeonService(IDungeonRepository dungeonRepository)
    {
        _dungeonRepository = dungeonRepository;
    }

    public async Task<IEnumerable<Boss>> GetBossesAsync(int dungeonId)
    {
        return await _dungeonRepository.GetBossesByDungeonId(dungeonId);
    }

}
