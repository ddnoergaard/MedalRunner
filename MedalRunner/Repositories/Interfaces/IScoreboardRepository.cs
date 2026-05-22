using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IScoreboardRepository
    {
        Task CreateAsync(Scoreboard scoreboard);
        Task<List<Scoreboard>> GetAllScores();
        Task<Scoreboard> GetScoreById(int id);
        Task<IEnumerable<Scoreboard>> GetScoreboardsOnDungeonIdAsync(int dungeonId);
        Task UpdateScore(Scoreboard score);
        Task SetInactive(int id);
        Task SetActive(int id);
        Task<IEnumerable<Scoreboard>> GetScoreboardRecordsByUserId(int userId);
        Task<int> GetScoreboardCount();
        Task<IEnumerable<Scoreboard>> GetFiveLatestScoreboards();
    }
}
