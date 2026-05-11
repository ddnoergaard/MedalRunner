using MedalRunner.Models;

namespace MedalRunner.Repositories.Interfaces
{
    public interface IScoreboardRepository
    {
        Task<List<Scoreboard>> GetAllScores();
        Task<Scoreboard> GetScoreById(int id);
        Task UpdateScore(Scoreboard score);
        Task SetInactive(int id);
        Task SetActive(int id);
    }
}
