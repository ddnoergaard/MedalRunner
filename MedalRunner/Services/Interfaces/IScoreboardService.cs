using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IScoreboardService
    {
        Task CreateAsync(Scoreboard scoreboard);
        Task<List<Scoreboard>> GetAllScores();
        Task<Scoreboard> GetScoreById(int id);
        Task<IEnumerable<Scoreboard>> GetScoreboardsOnDungeonIdAsync(int dungeonId);
        Task Update(Scoreboard score);
        Task SetInactive(int id);
        Task SetActive(int id);
        Task<IEnumerable<Scoreboard>> NameSearch(string str);
        Task<IEnumerable<Scoreboard>> GetScoreboardRecordsByUserId(int userId);
        Task<int> GetScoreboardCount();
        Task<IEnumerable<Scoreboard>> GetFiveLatestScoreboards();

    }
}
