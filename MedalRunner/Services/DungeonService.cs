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

    public async Task AddDungeon(Dungeon dungeon)
    {
        await _dungeonRepository.AddDungeon(dungeon);
    }

    public async Task UpdateDungeon(Dungeon dungeon)
    {
        await _dungeonRepository.UpdateDungeon(dungeon);
    }

    public async Task DeleteDungeon(int id)
    {
        await _dungeonRepository.DeleteDungeon(id);
    }
}
