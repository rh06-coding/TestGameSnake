using SnakeGame.Models;
using System;

namespace SnakeGame.Database
{
    public static class SessionManager
    {
        public static TaiKhoan CurrentUser { get; set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static void Login(TaiKhoan user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        /// <summary>
        /// Lưu điểm số khi game over và cập nhật HighestScore nếu cần
        /// </summary>
        /// <param name="score">Điểm số đạt được</param>
        /// <param name="mapType">Loại map (1 hoặc 2)</param>
        /// <returns>True nếu lưu thành công</returns>
        public static bool SaveGameScore(int score, int mapType)
        {
            if (!IsLoggedIn)
            {
                System.Diagnostics.Debug.WriteLine("❌ User not logged in, cannot save score");
                return false;
            }

            if (score <= 0)
            {
                System.Diagnostics.Debug.WriteLine("❌ Invalid score value, cannot save");
                return false;
            }

            try
            {
                var scoreRepo = new ScoreRepository();
                var taiKhoanRepo = new TaiKhoanRepository();

                // 1. Lưu điểm vào bảng SCORES với mapType
                bool saved = scoreRepo.AddScore(CurrentUser.PlayerID, score, mapType);

                if (!saved)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Failed to save score {score} (Map {mapType}) to database");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"✅ Score {score} (Map {mapType}) saved to SCORES table");

                // 2. Cập nhật HighestScore nếu điểm mới cao hơn
                if (score > CurrentUser.HighestScore)
                {
                    bool updated = taiKhoanRepo.UpdateHighestScore(CurrentUser.PlayerID, score);
                    
                    if (updated)
                    {
                        CurrentUser.HighestScore = score; // Update local cache
                        System.Diagnostics.Debug.WriteLine($"✅ New highest score updated: {score}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"⚠️ Failed to update highest score");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ SaveGameScore Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lấy thống kê của người chơi hiện tại
        /// </summary>
        /// <returns>PlayerStats object hoặc null nếu không đăng nhập</returns>
        public static PlayerStats GetPlayerStats()
        {
            if (!IsLoggedIn)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ User not logged in, cannot get stats");
                return null;
            }

            try
            {
                var scoreRepo = new ScoreRepository();
                
                var stats = new PlayerStats
                {
                    PlayerID = CurrentUser.PlayerID,
                    Username = CurrentUser.Username,
                    TotalGames = scoreRepo.GetTotalGamesPlayed(CurrentUser.PlayerID),
                    AverageScore = scoreRepo.GetAverageScore(CurrentUser.PlayerID),
                    HighestScore = CurrentUser.HighestScore
                };

                System.Diagnostics.Debug.WriteLine($"✅ Stats loaded: {stats.TotalGames} games, Avg: {stats.AverageScore:F1}, Best: {stats.HighestScore}");
                return stats;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ GetPlayerStats Error: {ex.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// Model chứa thống kê của người chơi
    /// </summary>
    public class PlayerStats
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public int TotalGames { get; set; }
        public double AverageScore { get; set; }
        public int HighestScore { get; set; }

        /// <summary>
        /// Format hiển thị thống kê
        /// </summary>
        public override string ToString()
        {
            return $"Player: {Username} | Games: {TotalGames} | Avg: {AverageScore:F1} | Best: {HighestScore}";
        }
    }
}
