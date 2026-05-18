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
                "VALUES (@firstName, @lastName, @email, @password, @roleId, @subscriptionId, @subscriptionStart, @createdAt)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@roleId", user.RoleId);
                    cmd.Parameters.AddWithValue("@subscriptionId", user.SubscriptionId);
                    cmd.Parameters.AddWithValue("@subscriptionStart", DateTime.UtcNow);
                    cmd.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);

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

        public async Task<User> GetUserByEmail(string Email)
        {
            string sqlQuery = "SELECT email, password FROM users WHERE email = @Email";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    try
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                            if (await reader.ReadAsync())
                            {
                                return new User
                                {
                                    Email = Convert.ToString(reader["email"]),
                                    Password = Convert.ToString(reader["password"])
                                };
                            }
                        throw new ArgumentException("No User with that Email found");
                    }
                    catch (SqlException)
                    { 
                        throw;
                    }
                }
            }
        }

    }
}
