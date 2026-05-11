using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Models;
using MedalRunner.Repositories;
using MedalRunner.Pages.Public_pages.Dungeons;
using Microsoft.Data.SqlClient;

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
        await _dungeonRepository.AddDungeonAsync(dungeon);
    }

    public async Task UpdateDungeon(Dungeon dungeon)
    {
        await _dungeonRepository.UpdateDungeonAsync(dungeon);
    }

    public async Task DeleteDungeon(int id)
    {
        await _dungeonRepository.DeleteDungeonAsync(id);
    }

    public async Task<List<Dungeon>> GetAllDungeons()
    {
        return await _dungeonRepository.GetAllDungeonsAsync(); 
    }

    public async Task<IEnumerable<Boss>> GetBossesAsync(int dungeonId)
    {
        return await _dungeonRepository.GetBossesByDungeonIdAsync(dungeonId);
    }

    public async Task<Dungeon> GetDungeonByIdAsync(int id)
    {
        try
        {
            return await _dungeonRepository.GetDungeonByIdAsync(id);
        } catch (SqlException)
        {
            throw;
        }
    }

}
