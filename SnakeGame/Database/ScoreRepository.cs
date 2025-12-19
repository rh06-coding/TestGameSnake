using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SnakeGame.Models;

namespace SnakeGame.Database
{
    public class ScoreRepository
    {
        // Thêm ?i?m m?i
        public bool AddScore(int playerID, int scoreValue)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO SCORES (player_ID, score) 
                                   VALUES (@playerID, @score)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);
                        cmd.Parameters.AddWithValue("@score", scoreValue);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i thêm ?i?m: {ex.Message}");
            }
        }

        // L?y t?t c? ?i?m c?a m?t ng??i ch?i
        public List<Score> GetScoresByPlayerID(int playerID)
        {
            List<Score> scores = new List<Score>();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT score_ID, player_ID, score, AchievedAt 
                                   FROM SCORES 
                                   WHERE player_ID = @playerID 
                                   ORDER BY AchievedAt DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                scores.Add(new Score
                                {
                                    ScoreID = reader.GetInt32(0),
                                    PlayerID = reader.GetInt32(1),
                                    ScoreValue = reader.GetInt32(2),
                                    AchievedAt = reader.GetDateTime(3)
                                });
                            }
                        }
                    }
                }
                return scores;
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i l?y danh sách ?i?m: {ex.Message}");
            }
        }

        // L?y top ?i?m cao nh?t (Leaderboard)
        public List<(string Username, int HighestScore, DateTime JoinDate)> GetTopScores(int topN = 10)
        {
            List<(string, int, DateTime)> topScores = new List<(string, int, DateTime)>();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT TOP (@topN) username, HighestScore, JoinDate 
                                   FROM TAIKHOAN 
                                   ORDER BY HighestScore DESC, JoinDate ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@topN", topN);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topScores.Add((
                                    reader.GetString(0),
                                    reader.GetInt32(1),
                                    reader.GetDateTime(2)
                                ));
                            }
                        }
                    }
                }
                return topScores;
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i l?y b?ng x?p h?ng: {ex.Message}");
            }
        }

        // L?y ?i?m trung bình c?a ng??i ch?i
        public double GetAverageScore(int playerID)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT AVG(CAST(score AS FLOAT)) 
                                   FROM SCORES 
                                   WHERE player_ID = @playerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);

                        object result = cmd.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDouble(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i tính ?i?m trung bình: {ex.Message}");
            }
        }

        // ??m s? l?n ch?i
        public int GetTotalGamesPlayed(int playerID)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM SCORES WHERE player_ID = @playerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i ??m s? ván ch?i: {ex.Message}");
            }
        }
    }
}
