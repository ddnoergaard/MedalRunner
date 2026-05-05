using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;

namespace MedalRunner.Repositories
{
    public class ConsumableRepository : IConsumableRepository
    {
        private string _connectionString;

        public ConsumableRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
