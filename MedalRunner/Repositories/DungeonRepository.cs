using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Reflection.PortableExecutable;

namespace MedalRunner.Repositories
{
    public class DungeonRepository : IDungeonRepository
    {
        private string _connectionString;

        public DungeonRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Dungeon>> GetAllDungeonsAsync()
        {
            //Create a list to store all the dungeons that we retrieve from the database
            List<Dungeon> data = new List<Dungeon>();
            //Create Instance of dungeon for later use
            Dungeon dungeon = new Dungeon();

            string sqlQuery = "Select * FROM Dungeons";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                try
                {
                    await cmd.ExecuteScalarAsync();
                }
                catch (SqlException ex)
                {
                    throw;
                }
                while (await reader.ReadAsync())
                    {
                        dungeon.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        dungeon.Name = reader.GetString(reader.GetOrdinal("name"));
                        dungeon.Zone = reader.GetString(reader.GetOrdinal("zone"));
                        dungeon.Description = reader.GetString(reader.GetOrdinal("description"));
                        dungeon.Platinum = reader.GetBoolean(reader.GetOrdinal("platinum"));
                        dungeon.Gold = reader.GetBoolean(reader.GetOrdinal("gold"));
                        dungeon.Silver = reader.GetBoolean(reader.GetOrdinal("silver"));
                        dungeon.Bronze = reader.GetBoolean(reader.GetOrdinal("bronze"));
                        dungeon.MopAmount = reader.GetInt32(reader.GetOrdinal("mop_amount"));

                        data.Add(dungeon);
                    }
                    return data;
               

            }
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