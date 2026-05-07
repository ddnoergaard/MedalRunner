using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddUser(User user)
        {
            string sqlQuery = "INSERT INTO users(first_name, last_name, email, password, role_id, subscription_id, subscription_start, created_at)" +
                "VALUES (first_name = @firstName, last_name = @lastName, email = @email, password = @password, role_id = @roleId, subscription_id = @subscriptionId, subscription_start = @subscriptionId, created_at = @createdAt)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("first_name", user.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role_id", user.RoleId);
                    cmd.Parameters.AddWithValue("@subscription_id", user.SubscriptionId);
                    cmd.Parameters.AddWithValue("@subscription_start", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@created_at", DateTime.UtcNow);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    } catch (SqlException ex)
                    {
                        throw;
                    }

                }

            }

        }

    }
}
