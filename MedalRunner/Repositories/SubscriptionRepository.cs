using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private string _connectionString;

        public SubscriptionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Subscription>> GetAllSub()
        {
            List<Subscription> subscriptions = new List<Subscription>();
            string sqlQuery = "SELECT id, name, price, max_characters FROM subscriptions";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                try
                {
                    while (await reader.ReadAsync())
                    {
                        Subscription subscription = new Subscription();
                        subscription.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        subscription.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                        subscription.Name = reader.GetString(reader.GetOrdinal("name"));
                        subscription.MaxCharacters = reader.GetInt32(reader.GetOrdinal("max_characters"));
                        subscriptions.Add(subscription);
                    }
                }
                catch(SqlException ex)
                {
                    throw;
                }
                return subscriptions;
            }
        }
    }
}
