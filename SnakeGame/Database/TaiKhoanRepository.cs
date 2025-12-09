using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using SnakeGame.Models;

namespace SnakeGame.Database
{
    public class TaiKhoanRepository
    {
        // Mã hóa m?t kh?u b?ng SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // ??ng ký tài kho?n m?i
        public bool Register(string username, string password, string email)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO TAIKHOAN (username, matkhau, email) 
                                   VALUES (@username, @matkhau, @email)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@matkhau", HashPassword(password));
                        cmd.Parameters.AddWithValue("@email", email);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Username ho?c Email ?ã t?n t?i
                if (ex.Number == 2627) // Unique constraint violation
                {
                    throw new Exception("Username ho?c Email ?ã t?n t?i!");
                }
                throw new Exception($"L?i ??ng ký: {ex.Message}");
            }
        }

        // ??ng nh?p
        public TaiKhoan Login(string username, string password)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT player_ID, username, email, JoinDate, HighestScore 
                                   FROM TAIKHOAN 
                                   WHERE username = @username AND matkhau = @matkhau";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@matkhau", HashPassword(password));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new TaiKhoan
                                {
                                    PlayerID = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    JoinDate = reader.GetDateTime(3),
                                    HighestScore = reader.GetInt32(4)
                                };
                            }
                        }
                    }
                }
                return null; // ??ng nh?p th?t b?i
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i ??ng nh?p: {ex.Message}");
            }
        }

        // Ki?m tra username ?ã t?n t?i
        public bool IsUsernameExists(string username)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE username = @username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i ki?m tra username: {ex.Message}");
            }
        }

        // Ki?m tra email ?ã t?n t?i
        public bool IsEmailExists(string email)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TAIKHOAN WHERE email = @email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i ki?m tra email: {ex.Message}");
            }
        }

        // ??i m?t kh?u (cho Forgot Password)
        public bool ResetPassword(string email, string newPassword)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE TAIKHOAN SET matkhau = @matkhau WHERE email = @email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@matkhau", HashPassword(newPassword));
                        cmd.Parameters.AddWithValue("@email", email);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i ??i m?t kh?u: {ex.Message}");
            }
        }

        // C?p nh?t ?i?m cao nh?t
        public bool UpdateHighestScore(int playerID, int newScore)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE TAIKHOAN 
                                   SET HighestScore = @score 
                                   WHERE player_ID = @playerID AND HighestScore < @score";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);
                        cmd.Parameters.AddWithValue("@score", newScore);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i c?p nh?t ?i?m: {ex.Message}");
            }
        }

        // L?y thông tin tài kho?n theo ID
        public TaiKhoan GetPlayerByID(int playerID)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT player_ID, username, email, JoinDate, HighestScore 
                                   FROM TAIKHOAN 
                                   WHERE player_ID = @playerID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@playerID", playerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new TaiKhoan
                                {
                                    PlayerID = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    JoinDate = reader.GetDateTime(3),
                                    HighestScore = reader.GetInt32(4)
                                };
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i l?y thông tin: {ex.Message}");
            }
        }
    }
}
