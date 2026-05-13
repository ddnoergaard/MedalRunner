using MedalRunner.Models;

namespace MedalRunner.Services.Interfaces
{
    public interface IScoreboardService
    {
        Task<List<Scoreboard>> GetAllScores();
        Task<Scoreboard> GetScoreById(int id);
        Task<IEnumerable<Scoreboard>> GetScoreboardsOnDungeonIdAsync(int dungeonId);
        Task Update(Scoreboard score);
        Task SetInactive(int id);
        Task SetActive(int id);
        Task<IEnumerable<Scoreboard>> NameSearch(string str);

    }
}
