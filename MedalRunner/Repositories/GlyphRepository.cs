using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Repositories
{
    public class GlyphRepository : IGlyphRepository
    {
        private string _connectionString;

        public GlyphRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
