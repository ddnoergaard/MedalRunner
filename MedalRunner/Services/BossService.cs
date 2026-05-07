using MedalRunner.Services.Interfaces;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Models;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class BossService : IBossService
    {
        private readonly IBossRepository _bossRepository;

        public BossService(IBossRepository bossRepository)
        {
            _bossRepository = bossRepository;
        }

        public async Task AddAsync(Boss boss, int dungeonId)
        {
            try
            {
                await _bossRepository.AddAsync(boss, dungeonId);
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Boss>> GetBossesAsync()
        {
            try
            {
                return await _bossRepository.GetAllBossesAsync();
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _bossRepository.DeleteAsync(id);
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task UpdateImageUrl(int bossId, string imageUrl)
        {
            try
            {
                await _bossRepository.UpdateImageUrl(bossId, imageUrl);
            } catch(SqlException)
            {
                throw;
            }
        }

        public async Task<Boss> GetBossById(int id)
        {
            try
            {
                return await _bossRepository.GetBossByIdAsync(id);
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task<Boss> GetBossByNameAsync(string name)
        {
            try
            {
                return await _bossRepository.GetBossByNameAsync(name);
            } catch (SqlException)
            {
                throw;
            }
        }
    }
}
