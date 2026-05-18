using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
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
        try
        {
            await _dungeonRepository.AddDungeonAsync(dungeon);
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public async Task UpdateDungeon(Dungeon dungeon)
    {
        try
        {
            await _dungeonRepository.UpdateDungeonAsync(dungeon);
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public async Task DeleteDungeon(int id)
    {
        try
        {
            await _dungeonRepository.DeleteDungeonAsync(id);
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public async Task<List<Dungeon>> GetAllDungeons()
    {
        try
        {
            return await _dungeonRepository.GetAllDungeonsAsync(); 
        }
        catch (IndexOutOfRangeException)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Boss>> GetBossesAsync(int dungeonId)
    {
        try
        {
            return await _dungeonRepository.GetBossesByDungeonIdAsync(dungeonId);
        }
        catch (IndexOutOfRangeException)
        {
            throw;
        }
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
