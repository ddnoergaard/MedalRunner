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

        public async Task<int> AddAsync(Character character)
        {
            string sql = @"
                INSERT INTO characters (name, race, characterClass, specialization, createdAt)
                OUTPUT INSERTED.id
                VALUES (@Name, @Race, @CharacterClass, @Specialization, @CreatedAt);";

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200) { Value = character.Name });
            cmd.Parameters.Add(new SqlParameter("@Race", SqlDbType.NVarChar, 100) { Value = character.Race });
            cmd.Parameters.Add(new SqlParameter("@CharacterClass", SqlDbType.NVarChar, 100) { Value = character.CharacterClass });
            cmd.Parameters.Add(new SqlParameter("@Specialization", SqlDbType.Int) { Value = character.Specialization });
            cmd.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.Date) { Value = character.CreatedAt });

            try
            {
                return Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
            catch (SqlException ex)
            {
                throw;
            }


        }

        public async Task DeleteAsync(int id)
        {
            string sql = "DELETE FROM characters WHERE id = @Id";

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Character>> GetAllAsync()
        {
            var List = new List<Character>();
            string sql = @"SELECT id, name, race, class_id, spec_id, create_time FROM characters ORDER BY name";

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);


            try
            {
                await using var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (await rdr.ReadAsync())
                {
                    List.Add(MapReaderToCharacter(rdr));
                }

                return List;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public async Task<Character> GetByIdAsync(int id)
        {
            string sql = @"SELECT id, name, race, class_id, spec_id, create_time FROM Characters WHERE id = @Id";

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });

            await using var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow);
            if (await rdr.ReadAsync())
                return MapReaderToCharacter(rdr);

            return null;
        }

        public async Task UpdateAsync(Character character)
        {
            string sql = @"
                UPDATE characters
                SET name = @Name,
                    race = @Race,
                    class_id = @CharacterClass,
                    spec_id = @Specialization
                WHERE id = @Id";

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200) { Value = character.Name });
            cmd.Parameters.Add(new SqlParameter("@Race", SqlDbType.NVarChar, 100) { Value = character.Race });
            cmd.Parameters.Add(new SqlParameter("@CharacterClass", SqlDbType.NVarChar, 100) { Value = character.CharacterClass });
            cmd.Parameters.Add(new SqlParameter("@Specialization", SqlDbType.Int) { Value = character.Specialization });
            cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = character.Id });

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        private static Character MapReaderToCharacter(SqlDataReader rdr)
        {
            return new Character
            {
                Id = rdr.GetInt32(rdr.GetOrdinal("id")),
                Name = rdr.GetString(rdr.GetOrdinal("name")),
                //Find better way to do this
                Race = rdr.IsDBNull(rdr.GetOrdinal("race")) ? null : rdr.GetString(rdr.GetOrdinal("race")),
                Specialization = rdr.GetInt32(rdr.GetOrdinal("spec_id")),
                CreatedAt = rdr.GetDateTime(rdr.GetOrdinal("created_time"))
            };
        }
    }
}
