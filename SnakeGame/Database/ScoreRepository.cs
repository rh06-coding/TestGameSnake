using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SnakeGame.Models;

namespace SnakeGame.Database
{
    public class ScoreRepository
    {
        // Thêm điểm mới
        public bool AddScore(int playerID, int scoreValue, int mapType)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO SCORES (player_ID, score, mapType) 
                                   VALUES (@playerID, @score, @mapType)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);
                        cmd.Parameters.AddWithValue("@score", scoreValue);
                        cmd.Parameters.AddWithValue("@mapType", mapType);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error in AddScore: {ex.Message}");
                throw new Exception($"Lỗi thêm điểm: Không thể kết nối database");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AddScore: {ex.Message}");
                throw new Exception($"Lỗi thêm điểm: {ex.Message}");
            }
        }

        // Lấy tất cả điểm của một người chơi
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error in GetScoresByPlayerID: {ex.Message}");
                throw new Exception($"Lỗi lấy danh sách điểm: Không thể kết nối database");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetScoresByPlayerID: {ex.Message}");
                throw new Exception($"Lỗi lấy danh sách điểm: {ex.Message}");
            }
        }

        // Lấy top điểm cao nhất (Leaderboard)
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error in GetTopScores: {ex.Message}");
                throw new Exception($"Lỗi lấy bảng xếp hạng: Không thể kết nối database");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetTopScores: {ex.Message}");
                throw new Exception($"Lỗi lấy bảng xếp hạng: {ex.Message}");
            }
        }

        // Lấy top điểm cao nhất theo map (Leaderboard theo map)
        public List<(string Username, int HighestScore, DateTime JoinDate)> GetTopScoresByMap(int mapType, int topN = 10)
        {
            List<(string, int, DateTime)> topScores = new List<(string, int, DateTime)>();

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT TOP (@topN) 
                                        t.username, 
                                        MAX(s.score) AS HighestScore, 
                                        t.JoinDate
                                    FROM TAIKHOAN t
                                    INNER JOIN SCORES s ON t.player_ID = s.player_ID
                                    WHERE s.mapType = @mapType
                                    GROUP BY t.username, t.JoinDate
                                    ORDER BY MAX(s.score) DESC, t.JoinDate ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@topN", topN);
                        cmd.Parameters.AddWithValue("@mapType", mapType);

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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error in GetTopScoresByMap: {ex.Message}");
                throw new Exception($"Lỗi lấy bảng xếp hạng theo map: Không thể kết nối database");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetTopScoresByMap: {ex.Message}");
                throw new Exception($"Lỗi lấy bảng xếp hạng theo map: {ex.Message}");
            }
        }

        // Lấy điểm trung bình của người chơi
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error in GetAverageScore: {ex.Message}");
                throw new Exception($"Lỗi tính điểm trung bình: Không thể kết nối database");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAverageScore: {ex.Message}");
                throw new Exception($"Lỗi tính điểm trung bình: {ex.Message}");
            }
        }

        // Đếm số lần chơi
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
            catch (SqlException ex)
            {
                System.Diagnostics.Debug.WriteLine($"SQL Error in GetTotalGamesPlayed: {ex.Message}");
                throw new Exception($"Lỗi đếm số ván chơi: Không thể kết nối database");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetTotalGamesPlayed: {ex.Message}");
                throw new Exception($"Lỗi đếm số ván chơi: {ex.Message}");
            }
        }
    }
}
