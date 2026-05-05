using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Repositories
{
    public class BossRepository : IBossRepository
    {
        private string _connectionString;

        public BossRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

    }
}
