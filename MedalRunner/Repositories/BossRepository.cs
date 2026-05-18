using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using System.Security.Cryptography.X509Certificates;

namespace MedalRunner.Repositories
{
    public class BossRepository : IBossRepository
    {
        private string _connectionString;

        public BossRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Adds to regular boss table and junction table "dungeon_bosses"
        /// </summary>
        /// <param name="boss"></param>
        /// <returns></returns>
        public async Task AddAsync(Boss boss, int dungeonId)
        {
            string sqlQueryBoss = "INSERT INTO bosses(name, image_url) OUTPUT INSERTED.id VALUES (@name, @image_url)";
            string sqlQueryJunction = "INSERT INTO dungeon_bosses(boss_id, dungeon_id) VALUES (@bossId, @dungeonId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                int newBossId = 0;

                try
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQueryBoss, con))
                    {
                        cmd.Parameters.AddWithValue("@name", boss.Name);
                        cmd.Parameters.AddWithValue("@image_url", boss.ImageUrl);
                        newBossId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }

                    using (SqlCommand junctionCmd = new SqlCommand(sqlQueryJunction, con))
                    {
                        junctionCmd.Parameters.AddWithValue("@bossId", newBossId);
                        junctionCmd.Parameters.AddWithValue("@dungeonId", dungeonId);
                        await junctionCmd.ExecuteNonQueryAsync();
                    }
                } catch(SqlException)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Boss>> GetAllBossesAsync()
        {
            string sqlQuery = "SELECT * FROM bosses";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    List<Boss> tempList = new List<Boss>();
                    using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Boss tempBoss = new Boss
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = Convert.ToString(reader["name"]),
                                ImageUrl = Convert.ToString(reader["image_url"])
                            };
                            tempList.Add(tempBoss);
                        }
                        return tempList;
                    }
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            string sqlQuery = "DELETE FROM bosses WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

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

        public async Task UpdateImageUrlAsync(int bossId, string imageUrl)
        {
            string sqlQuery = "UPDATE bosses SET image_url = @imageUrl WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@imageUrl", imageUrl);
                    cmd.Parameters.AddWithValue("@id", bossId);
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

        public async Task<Boss> GetBossByIdAsync(int id)
        {
            string sqlQuery = "SELECT * from bosses WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!reader.Read())
                        {
                            throw new KeyNotFoundException("No boss found with that id");
                        }

                        return new Boss
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            ImageUrl = Convert.ToString(reader["image_url"])
                        };
                    }
                }
            }
        }

        public async Task<Boss> GetBossByNameAsync(string name)
        {
            string sqlQuery = "SELECT * FROM bosses WHERE name = @name";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if(!reader.Read())
                        {
                            throw new KeyNotFoundException("No boss with that name found");
                        }

                        return new Boss
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            ImageUrl = Convert.ToString(reader["image_url"])
                        };
                    }
                }
            }
        }

        public async Task<IEnumerable<Boss>> GetBossesByItemId(int id)
        {
            string sqlQuery = "SELECT boss_id FROM boss_drops WHERE item_id = @id";
            List<int> bossIds = new List<int>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bossIds.Add(Convert.ToInt32(reader["boss_id"]));
                        }
                    }
                }

                if (bossIds.Count == 0) throw new ArgumentException("No bosses associated with that item id");

                string bossQuery = $"SELECT * FROM bosses WHERE id IN ({string.Join(", ", bossIds)})";
                List<Boss> bossList = new List<Boss>();


                using (SqlCommand cmdBoss = new SqlCommand(bossQuery, con))
                {
                    using (SqlDataReader reader = await cmdBoss.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            bossList.Add(new Boss
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = Convert.ToString(reader["name"]),
                                ImageUrl = Convert.ToString(reader["image_url"])
                            });
                        }
                        if (bossList.Count == 0) throw new ArgumentException("No bosses found");
                    }
                }
                return bossList;
            }
        }

    }
}
