using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class DungeonRepository : IDungeonRepository
    {
        private string _connectionString;

        public DungeonRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Boss>> GetBossesByDungeonId(int dungeonId)
        {
            List<int> bossIds = new List<int>();
            List<Boss> bosses = new List<Boss>();
            string sqlQueryJunction = "SELECT boss_id FROM dungeon_bosses WHERE dungeon_id = @dungeonId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQueryJunction, con))
                {
                    cmd.Parameters.AddWithValue("@dungeonId", dungeonId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            bossIds.Add(Convert.ToInt32(reader["boss_id"]));
                        }
                    }
                }

                if (bossIds.Count == 0)
                {
                    return bosses;
                }

                string sqlQueryBosses = $"SELECT * FROM bosses WHERE id IN ({string.Join(", ", bossIds)})";

                using (SqlCommand cmd = new SqlCommand(sqlQueryBosses, con))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bosses.Add(new Boss
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = Convert.ToString(reader["name"]),
                                ImageUrl = Convert.ToString(reader["image_url"])
                            });
                        }
                        return bosses;
                    }
                    
                }

            }

        }
    }
}
