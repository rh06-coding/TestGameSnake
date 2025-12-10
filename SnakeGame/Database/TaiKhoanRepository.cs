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
        // Mã hóa mật khẩu bằng SHA256
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

        // Đăng ký tài khoản mới
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
                // Username hoặc Email đã tồn tại
                if (ex.Number == 2627) // Unique constraint violation
                {
                    throw new Exception("Username hoặc Email đã tồn tại!");
                }
                throw new Exception($"Lỗi đăng ký: {ex.Message}");
            }
        }

        // Đăng nhập
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
                return null; // Đăng nhập thất bại
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đăng nhập: {ex.Message}");
            }
        }

        // Kiểm tra username đã tồn tại
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
                throw new Exception($"Lỗi kiểm tra username: {ex.Message}");
            }
        }

        // Kiểm tra email đã tồn tại
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
                throw new Exception($"Lỗi kiểm tra email: {ex.Message}");
            }
        }

        // Đổi mật khẩu (cho Forgot Password)
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
                throw new Exception($"Lỗi đổi mật khẩu: {ex.Message}");
            }
        }

        // Cập nhật điểm cao nhất
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
                throw new Exception($"Lỗi cập nhật điểm: {ex.Message}");
            }
        }

        // Lấy thông tin tài khoản theo ID
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
                throw new Exception($"Lỗi lấy thông tin: {ex.Message}");
            }
        }
    }
}
