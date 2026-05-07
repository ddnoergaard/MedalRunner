using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Services
{
    public class ScoreboardService : IScoreboardService
    {
        private readonly IScoreboardRepository _scoreboardRepository;

        public ScoreboardService(IScoreboardRepository scoreboardRepository)
        {
            _scoreboardRepository = scoreboardRepository;
        }

        public async Task<List<Scoreboard>> GetAllScores()
        {
            return await _scoreboardRepository.GetAllScores();
        }

        public async Task Update(Scoreboard score)
        {
            _scoreboardRepository.UpdateScore(score);
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
    }
}
