using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Repositories
{
    public class DungeonRepository : IDungeonRepository
    {
        private string _connectionString;

        public DungeonRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
