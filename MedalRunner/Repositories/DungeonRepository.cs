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

        public async Task<List<Dungeon>> GetAllDungeons()
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
                        dungeon.Mop_amount = reader.GetInt32(reader.GetOrdinal("mop_amount"));

                        data.Add(dungeon);
                    }
                    return data;
                }
                catch(SqlException ex)
                {
                    throw;
                }

            }
        }
    }
}
