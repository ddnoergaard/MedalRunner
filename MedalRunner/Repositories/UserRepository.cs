using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;

        public UserRepository(string conString)
        {
            _connectionString = conString;
        }

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
                    } catch (SqlException)
                    {
                        throw;
                    }

                }

            }

        }

        public async Task<User> GetUserByEmail(string Email)
        {
            string sqlQuery = "SELECT * FROM users WHERE email = @Email";

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
                                    Password = Convert.ToString(reader["password"]),
                                    RoleId = Convert.ToInt32(reader["role_id"]),
                                    FirstName = Convert.ToString(reader["first_name"]),
                                    LastName = Convert.ToString(reader["last_name"]),
                                    SubscriptionId = Convert.ToInt32(reader["subscription_id"]),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                    Id = Convert.ToInt32(reader["id"])
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

        public async Task DeleteUserById(int id)
        {
            string sqlQuery = "DELETE FROM users WHERE id = @userId";
            int count = 0;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@userId", id);
                    try
                    {
                        count = await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    if (count == 0) throw new ArgumentException("User couldn't be deleted");
                }
            }
        }

        public async Task<int> GetUserCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM users";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    try
                    {
                        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    } catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task<IEnumerable<User>> GetFiveLatestUsers()
        {
            string sqlQuery = "SELECT TOP(5) * FROM users ORDER BY created_at DESC";
            List<User> returnList = new List<User>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            returnList.Add(new User
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Email = Convert.ToString(reader["email"]),
                                Password = Convert.ToString(reader["password"]),
                                RoleId = Convert.ToInt32(reader["role_id"]),
                                FirstName = Convert.ToString(reader["first_name"]),
                                LastName = Convert.ToString(reader["last_name"]),
                                SubscriptionId = Convert.ToInt32(reader["subscription_id"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                        if (returnList.Count == 0) throw new IndexOutOfRangeException("No users found");
                        return returnList;
                    }
                }
            }
        }

    }
}
