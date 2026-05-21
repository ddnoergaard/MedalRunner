using MedalRunner.Models;
using MedalRunner.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace MedalRunner.Repositories
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private string _connectionString;

        public ScoreboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CreateAsync(Scoreboard scoreboard)
        {
            string sqlQuery = "INSERT INTO scoreboards(dungeon_id, name, score, created_at, is_active, run_date, user_id) " +
                "VALUES (@dungeon_id, @name, @score, @createdAt, @isActive, @runDate, @userId)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@dungeon_id", scoreboard.DungeonId);
                    cmd.Parameters.AddWithValue("@name", scoreboard.Name);
                    cmd.Parameters.AddWithValue("@score", scoreboard.Score);
                    cmd.Parameters.AddWithValue("@createdAt", scoreboard.CreatedAt);
                    cmd.Parameters.AddWithValue("@isActive", scoreboard.IsActive);
                    cmd.Parameters.AddWithValue("@runDate", scoreboard.RunDate);
                    cmd.Parameters.AddWithValue("@userId", scoreboard.UserId);

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

        public async Task<List<Scoreboard>> GetAllScores()
        {
            List<Scoreboard> data = new List<Scoreboard>();

            string sqlQuery = "SELECT * FROM scoreboards";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                try
                {
                    while (await reader.ReadAsync())
                    {
                        data.Add(new Scoreboard
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            DungeonId = Convert.ToInt32(reader["dungeon_id"]),
                            Name = Convert.ToString(reader["name"]),
                            Score = Convert.ToString(reader["score"]),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            IsActive = Convert.ToBoolean(reader["is_active"]),
                            RunDate = Convert.ToDateTime(reader["run_date"]),
                            UserId = Convert.ToInt32(reader["user_id"])
                        });
                        //Scoreboard score = new Scoreboard();
                        //score.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        //score.Name = reader.GetString(reader.GetOrdinal("name"));
                        //score.Score = reader.GetString(reader.GetOrdinal("score"));
                        //score.RunDate = reader.GetDateTime(reader.GetOrdinal("run_date"));
                        //score.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                        //score.IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"));
                        //data.Add(score);
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
            string sqlQuery = "SELECT * FROM scoreboards WHERE id = @id";

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
                            score.Id = Convert.ToInt32(reader["id"]);
                            score.DungeonId = Convert.ToInt32(reader["dungeon_id"]);
                            score.Name = Convert.ToString(reader["name"]);
                            score.Score = Convert.ToString(reader["score"]);
                            score.CreatedAt = Convert.ToDateTime(reader["created_at"]);
                            score.IsActive = Convert.ToBoolean(reader["is_active"]);
                            score.RunDate = Convert.ToDateTime(reader["run_date"]);
                            score.UserId = Convert.ToInt32(reader["user_id"]);
                            
                           
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

        public async Task<IEnumerable<Scoreboard>> GetScoreboardsOnDungeonIdAsync(int dungeonId)
        {
            string sqlQuery = "SELECT * FROM scoreboards WHERE dungeon_id = @dungeonId";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@dungeonId", dungeonId);
                    List<Scoreboard> returnList = new List<Scoreboard>();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            returnList.Add(new Scoreboard
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                DungeonId = Convert.ToInt32(reader["dungeon_id"]),
                                Name = Convert.ToString(reader["name"]),
                                Score = Convert.ToString(reader["score"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                IsActive = Convert.ToBoolean(reader["is_active"]),
                                RunDate = Convert.ToDateTime(reader["run_date"])
                            });
                        }
                        if (returnList.Count == 0)
                        {
                            throw new InvalidOperationException("No runs found");
                        }
                        return returnList;
                    }
                }

            }
        }

        public async Task UpdateScore(Scoreboard score)
        {
            string sqlQuery = "UPDATE scoreboards " +
                "SET name = @name, dungeon_id = @dungeon, score = @score, run_date = @runDate, is_active = @isActive " +
                "WHERE id = @id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@id", score.Id);
                    cmd.Parameters.AddWithValue("@name", score.Name);
                    cmd.Parameters.AddWithValue("@dungeon", score.DungeonId);
                    cmd.Parameters.AddWithValue("@score", score.Score);
                    cmd.Parameters.AddWithValue("@runDate", score.RunDate);
                    cmd.Parameters.AddWithValue("@isActive", score.IsActive);
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
            string sqlQuery = "UPDATE scoreboards " +
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
            string sqlQuery = "UPDATE scoreboards " +
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

        public async Task<IEnumerable<Scoreboard>> GetScoreboardRecordsByUserId(int userId)
        {
            string sqlQuery = "SELECT * FROM scoreboards WHERE user_id = @id";
            List<Scoreboard> scoreboardList = new List<Scoreboard>();

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
                            scoreboardList.Add(new Scoreboard
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                DungeonId = Convert.ToInt32(reader["dungeon_id"]),
                                Name = Convert.ToString(reader["name"]),
                                Score = Convert.ToString(reader["score"]),
                                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                IsActive = Convert.ToBoolean(reader["is_active"]),
                                RunDate = Convert.ToDateTime(reader["run_date"]),
                                UserId = Convert.ToInt32(reader["user_id"])
                            });
                        }
                    }
                }
                if (scoreboardList.Count == 0) throw new ArgumentException("No records found");
                return scoreboardList;
            }
        }

    }
}
