using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.Intrinsics.Arm;

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
                INSERT INTO characters (name, race, class_id, spec_id, create_time)
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

            await EquipDefaultGearAsync(newCharId);
        }

        // Default starter item IDs, one per available slot (lowest id per slot in the DB)
        private static readonly int[] DefaultItemIds = { 132, 1, 2, 3, 5, 78, 81, 80, 4, 77, 159, 6, 110 };

        private async Task EquipDefaultGearAsync(int characterId)
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            foreach (int itemId in DefaultItemIds)
            {
                string sql = "INSERT INTO character_gear (character_id, item_id) VALUES (@characterId, @itemId)";
                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@characterId", SqlDbType.Int) { Value = characterId });
                cmd.Parameters.Add(new SqlParameter("@itemId", SqlDbType.Int) { Value = itemId });
                await cmd.ExecuteNonQueryAsync();
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
            List<string> charIds = new List<string>();

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
                            charIds.Add($"'{reader["character_id"]}'");
                        }
                    }
                }

                if (charIds.Count == 0) throw new ArgumentException("No characters found");

                //if (charId.Count == 1) charId[0] = charId[0].Replace(",", "");

                //if (charId.Count > 1)
                //{
                //    int length = charId.Count;

                //    charId[length - 1] = charId[length - 1].Replace(",", "");
                //}

                string sqlQueryCharacter = $"SELECT * from characters WHERE id IN ({string.Join(", ", charIds)})";
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
                                CreatedAt = Convert.ToDateTime(reader["create_time"])
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

                    await using var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
                    if (await rdr.ReadAsync())
                    {
                        return MapReaderToCharacter(rdr);
                    }
                    else
                    {
                        throw new ArgumentException("No Character with ID");
                    }
                }
            } 
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
                Race = reader.IsDBNull(reader.GetOrdinal("race")) ? null : reader.GetString(reader.GetOrdinal("race")),
                Specialization = reader.GetInt32(reader.GetOrdinal("spec_id")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("create_time"))
                
            };
        }

        public async Task EquipItemAsync(int characterId, int oldItemId, int newItemId)
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            if (oldItemId == 0)
            {
                // Slot was empty — insert a new row
                string sql = "INSERT INTO character_gear (character_id, item_id) VALUES (@characterId, @newItemId)";
                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@characterId", SqlDbType.Int) { Value = characterId });
                cmd.Parameters.Add(new SqlParameter("@newItemId", SqlDbType.Int) { Value = newItemId });
                await cmd.ExecuteNonQueryAsync();
            }
            else
            {
                // Slot had an item — swap it
                string sql = "UPDATE character_gear SET item_id = @newItemId WHERE character_id = @characterId AND item_id = @oldItemId";
                await using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@characterId", SqlDbType.Int) { Value = characterId });
                cmd.Parameters.Add(new SqlParameter("@oldItemId", SqlDbType.Int) { Value = oldItemId });
                cmd.Parameters.Add(new SqlParameter("@newItemId", SqlDbType.Int) { Value = newItemId });
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<string> GetSpecNameById(int id)
        {
            string sqlQuery = "SELECT name FROM specs WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    try
                    {
                        return Convert.ToString(await cmd.ExecuteScalarAsync());
                    } catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }

        public async Task<int> GetCharacterCount()
        {
            string sqlQuery = "SELECT COUNT(*) FROM characters";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    try
                    {
                        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
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

