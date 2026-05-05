using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Repositories
{
    public class GemRepository : IGemRepository
    {
        private string _connectionString;

        public GemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
