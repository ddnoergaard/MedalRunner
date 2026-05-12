using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace MedalRunner.Repositories
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private string _connectionString;

        public ScoreboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Scoreboard>> GetAllScores()
        {
            List<Scoreboard> data = new List<Scoreboard>();

            string sqlQuery = "SELECT * FROM Scoreboard";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                try
                {
                    while (await reader.ReadAsync())
                    {
                        Scoreboard score = new Scoreboard();
                        score.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        score.Name = reader.GetString(reader.GetOrdinal("player_name"));
                        score.Score = reader.GetInt32(reader.GetOrdinal("score")).ToString();
                        score.RunDate = reader.GetDateTime(reader.GetOrdinal("run_date"));
                        score.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                        score.IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"));
                        data.Add(score);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                }

                return data;
            }
        }

        public async Task<Scoreboard> GetScoreById(int id)
        {
            Scoreboard score = new Scoreboard();
            string sqlQuery = "SELECT * FROM Scoreboard WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    try
                    {
                        if (await reader.ReadAsync())
                        {
                            score.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            score.Name = reader.GetString(reader.GetOrdinal("player_name"));
                            score.Score = reader.GetInt32(reader.GetOrdinal("score")).ToString();
                            score.RunDate = reader.GetDateTime(reader.GetOrdinal("run_date"));
                            score.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                            score.IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"));
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
                return score;
            }
        }

        public async Task UpdateScore(Scoreboard score)
        {
            string sqlQuery = "UPDATE Scoreboard " +
                "SET dungeon = @dungeon, score = @score, run_date = @runDate, created_at = @CreatedAt, is_active = @isActive, " +
                "WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@dungeon", score.Dungeon);
                    cmd.Parameters.AddWithValue("@score", score.Score);
                    cmd.Parameters.AddWithValue("@runDate", score.RunDate);
                    cmd.Parameters.AddWithValue("@CreatedAt", score.CreatedAt);

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
            }
        }

        public async Task SetInactive(int id)
        {
            string sqlQuery = "UPDATE Scoreboard " +
                "SET is_active = 0 " +
                "WHERE id = @id";

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
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
            }
        }

        public async Task SetActive(int id)
        {
            string sqlQuery = "UPDATE Scoreboard " +
                "SET is_active = 1 " +
                "WHERE id = @id";

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
                        Console.WriteLine($"SQL Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
