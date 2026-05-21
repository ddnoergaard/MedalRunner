using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MedalRunner.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private string _connectionString;

        public CharacterRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddAsync(Character character, int userId)
        {
            string sql = @"
                INSERT INTO characters (name, race, characterClass, specialization, createdAt)
                OUTPUT INSERTED.id
                VALUES (@Name, @Race, @CharacterClass, @Specialization, @CreatedAt);";
            int newCharId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", character.Name);
                    cmd.Parameters.AddWithValue("@Race", character.Race);
                    cmd.Parameters.AddWithValue("@CharacterClass", character.CharacterClass);
                    cmd.Parameters.AddWithValue("@Specialization", character.Specialization);
                    cmd.Parameters.AddWithValue("@CreatedAt", character.CreatedAt);
                    try
                    {
                        newCharId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
            }

            string sqlQueryJunction = "INSERT INTO user_characters(user_id, character_id) VALUES (@userId, @characterId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmdJunction = new SqlCommand(sqlQueryJunction, con))
                {
                    cmdJunction.Parameters.AddWithValue("@userId", userId);
                    cmdJunction.Parameters.AddWithValue("@characterId", newCharId);
                    try
                    {
                        await cmdJunction.ExecuteNonQueryAsync();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            string sql = "DELETE FROM characters WHERE id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task<IEnumerable<Character>> GetCharactersByUserId (int userId)
        {
            string sqlQuery = "SELECT character_id FROM user_characters WHERE user_id = @id";
            List<int> charId = new List<int>();
            

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            charId.Add(Convert.ToInt32(reader["character_id"]));
                        }
                    }
                }
                string sqlQueryCharacter = $"SELECT * from character WHERE id IN {string.Join(", ", charId)}";
                List<Character> charList = new List<Character>();
                using (SqlCommand cmd = new SqlCommand(sqlQueryCharacter, con))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            charList.Add(new Character
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ClassId = Convert.ToInt32(reader["class_id"]),
                                Specialization = Convert.ToInt32(reader["spec_id"]),
                                Name = Convert.ToString(reader["name"]),
                                Race = Convert.ToString(reader["race"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                    }
                }
                if (charList.Count == 0) throw new ArgumentException("No character found");
                return charList;
            }
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            var list = new List<Character>();
            string sql = "SELECT id, name, race, class_id, spec_id, create_time FROM characters ORDER BY name";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                    {
                        try
                        {
                            while (await rdr.ReadAsync())
                            {
                                list.Add(MapReaderToCharacter(rdr));
                            }

                            return list;
                        }
                        catch (SqlException)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            string sql = "SELECT id, name, race, class_id, spec_id, create_time FROM Characters WHERE id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await rdr.ReadAsync())
                        {
                            return MapReaderToCharacter(rdr);
                        }
                    }
                }
            }

            return null;
        }

        public async Task UpdateAsync(Character character)
        {
            string sql = @"UPDATE characters SET 
                name = @Name, 
                race = @Race,
                class_id = @CharacterClass,
                spec_id = @Specialization 
                WHERE id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", character.Name);
                    cmd.Parameters.AddWithValue("@Race", character.Race);
                    cmd.Parameters.AddWithValue("@CharacterClass", character.CharacterClass);
                    cmd.Parameters.AddWithValue("@Specialization", character.Specialization);
                    cmd.Parameters.AddWithValue("@Id", character.Id);
                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }

        private static Character MapReaderToCharacter(SqlDataReader reader)
        {
            return new Character
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Race = reader.GetString(reader.GetOrdinal("race")),
                Specialization = reader.GetInt32(reader.GetOrdinal("spec_id")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_time"))
            };
        }
    }
}
