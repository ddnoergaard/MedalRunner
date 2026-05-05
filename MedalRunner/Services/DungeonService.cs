using MedalRunner.Repositories.Interfaces;
using MedalRunner.Models;
using MedalRunner.Repositories;
using MedalRunner.Pages.Public_pages.Dungeons;

namespace MedalRunner.Services.Interfaces;

public class DungeonService : IDungeonService
{
    private readonly IDungeonRepository _dungeonRepository;

    public DungeonService(IDungeonRepository dungeonRepository)
    {
        _dungeonRepository = dungeonRepository;
    }

    public async Task<List<Dungeon>> GetAllDungeons()
    {
        return await _dungeonRepository.GetAllDungeons(); 
    }

}
