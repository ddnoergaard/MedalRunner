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

        public async Task AddDungeon(Dungeon dungeon)
        {
            string sqlQuery = "INSERT INTO dungeons(name, zone, description, platinum, gold, silver, bronze, mop_amount)" +
                "VALUES (name = @name, zone = @zone, description = @description, platinum = @platinum, gold = @gold, silver = @silver, bronze = @bronze, mop_amount = @mopAmount)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@name", dungeon.Name);
                    cmd.Parameters.AddWithValue("@zone", dungeon.Zone);
                    cmd.Parameters.AddWithValue("@description", dungeon.Description);
                    cmd.Parameters.AddWithValue("@platinum", dungeon.Platinum);
                    cmd.Parameters.AddWithValue("@gold", dungeon.Gold);
                    cmd.Parameters.AddWithValue("@silver", dungeon.Silver);
                    cmd.Parameters.AddWithValue("@bronze", dungeon.Bronze);
                    cmd.Parameters.AddWithValue("@mopAmount", dungeon.MopAmount);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task UpdateDungeon(Dungeon dungeon)
        {
            string sqlQuery = "UPDATE dungeons" +
                "SET name = @name" +
                "zone = @zone" +
                "description = @description" +
                "platinum = @platinum" +
                "gold = @gold" +
                "silver = @silver" +
                "bronze = @bronze" +
                "mop_amount = @mopAmount" +
                "WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", dungeon.Id);
                    cmd.Parameters.AddWithValue("@name", dungeon.Name);
                    cmd.Parameters.AddWithValue("@zone", dungeon.Zone);
                    cmd.Parameters.AddWithValue("@description", dungeon.Description);
                    cmd.Parameters.AddWithValue("@platinum", dungeon.Platinum);
                    cmd.Parameters.AddWithValue("@gold", dungeon.Gold);
                    cmd.Parameters.AddWithValue("@silver", dungeon.Silver);
                    cmd.Parameters.AddWithValue("@bronze", dungeon.Bronze);
                    cmd.Parameters.AddWithValue("@mopAmount", dungeon.MopAmount);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task DeleteDungeon(int id)
        {
            string sqlQuery = "DELETE FROM dungeons WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        throw;
                    }
                }
            }
        }
    }
}