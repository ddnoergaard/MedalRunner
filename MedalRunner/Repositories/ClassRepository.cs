using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private string _connectionString;

        public ClassRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
