using MedalRunner.Repositories.Interfaces;
using MedalRunner.Services.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<string> GetClassNameOnId(int id)
        {
            try
            {
                return await _classRepository.GetClassNameOnId(id);
            } catch (SqlException)
            {
                throw;
            }
        }

    }
}
