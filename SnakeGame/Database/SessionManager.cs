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
        /// L?u ?i?m s? khi game over và c?p nh?t HighestScore n?u c?n
        /// </summary>
        /// <param name="score">?i?m s? ??t ???c</param>
        /// <returns>True n?u l?u thành công</returns>
        public static bool SaveGameScore(int score)
        {
            if (!IsLoggedIn)
            {
                System.Diagnostics.Debug.WriteLine("?? User not logged in, cannot save score");
                return false;
            }

            if (score <= 0)
            {
                System.Diagnostics.Debug.WriteLine("?? Invalid score value, cannot save");
                return false;
            }

            try
            {
                var scoreRepo = new ScoreRepository();
                var taiKhoanRepo = new TaiKhoanRepository();

                // 1. L?u ?i?m vào b?ng SCORES
                bool saved = scoreRepo.AddScore(CurrentUser.PlayerID, score);

                if (!saved)
                {
                    System.Diagnostics.Debug.WriteLine($"? Failed to save score {score} to database");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"? Score {score} saved to SCORES table");

                // 2. C?p nh?t HighestScore n?u ?i?m m?i cao h?n
                if (score > CurrentUser.HighestScore)
                {
                    bool updated = taiKhoanRepo.UpdateHighestScore(CurrentUser.PlayerID, score);
                    
                    if (updated)
                    {
                        CurrentUser.HighestScore = score; // Update local cache
                        System.Diagnostics.Debug.WriteLine($"? New highest score updated: {score}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"?? Failed to update highest score");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"? SaveGameScore Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// L?y th?ng kê c?a ng??i ch?i hi?n t?i
        /// </summary>
        /// <returns>PlayerStats object ho?c null n?u không ??ng nh?p</returns>
        public static PlayerStats GetPlayerStats()
        {
            if (!IsLoggedIn)
            {
                System.Diagnostics.Debug.WriteLine("?? User not logged in, cannot get stats");
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

                System.Diagnostics.Debug.WriteLine($"? Stats loaded: {stats.TotalGames} games, Avg: {stats.AverageScore:F1}, Best: {stats.HighestScore}");
                return stats;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"? GetPlayerStats Error: {ex.Message}");
                return null;
            }
        }
    }

    /// <summary>
    /// Model ch?a th?ng kê c?a ng??i ch?i
    /// </summary>
    public class PlayerStats
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public int TotalGames { get; set; }
        public double AverageScore { get; set; }
        public int HighestScore { get; set; }

        /// <summary>
        /// Format hi?n th? th?ng kê
        /// </summary>
        public override string ToString()
        {
            return $"Player: {Username} | Games: {TotalGames} | Avg: {AverageScore:F1} | Best: {HighestScore}";
        }
    }
}
