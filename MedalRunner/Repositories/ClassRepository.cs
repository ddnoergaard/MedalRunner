using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private string _connectionString;

        public ClassRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> GetClassNameOnId(int id)
        {
            string sqlQuery = "SELECT name FROM character_classes WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        return Convert.ToString(await cmd.ExecuteScalarAsync());
                    } catch (SqlException)
                    {
                        throw;
                    }
                    
                }
            }

        }

    }
}
