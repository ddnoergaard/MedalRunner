using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly IScoreboardRepository _scoreboardRepository;

        public ScoreboardService(IScoreboardRepository scoreboardRepository)
        {
            _scoreboardRepository = scoreboardRepository;
        }

        public async Task CreateAsync(Scoreboard scoreboard)
        {
            try
            {
                await _scoreboardRepository.CreateAsync(scoreboard);
            } catch(SqlException)
            {
                throw;
            }
        }

        public async Task<List<Scoreboard>> GetAllScores()
        {
            try
            {
                return await _scoreboardRepository.GetAllScores();
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task<Scoreboard> GetScoreById(int id)
        {
            return await _scoreboardRepository.GetScoreById(id);
        }
        public async Task<IEnumerable<Scoreboard>> GetScoreboardsOnDungeonIdAsync(int dungeonId)
        {
            try
            {
                return await _scoreboardRepository.GetScoreboardsOnDungeonIdAsync(dungeonId);
            } catch (InvalidOperationException ex)
            {
                throw;
            }
        }

        public async Task Update(Scoreboard score)
        {
            try
            {
                await _scoreboardRepository.UpdateScore(score);
            } catch (SqlException)
            {
                throw;
            }
        }

        public async Task SetInactive(int id)
        {
            _scoreboardRepository.SetInactive(id);
        }

        public async Task SetActive(int id)
        {
            _scoreboardRepository.SetActive(id);
        }

        public async Task<IEnumerable<Scoreboard>> NameSearch(string str)
        {
            List<Scoreboard> scores = await _scoreboardRepository.GetAllScores();
            return scores.Where(s => s.Name.Contains(str, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Scoreboard>> GetScoreboardRecordsByUserId(int userId)
        {
            try
            {
                return await _scoreboardRepository.GetScoreboardRecordsByUserId(userId);
            } catch (ArgumentException)
            {
                throw;
            }
        }


    }
}
