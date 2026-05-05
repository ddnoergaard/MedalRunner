using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Repositories
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private string _connectionString;

        public ScoreboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
