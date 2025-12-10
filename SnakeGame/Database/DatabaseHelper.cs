    using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace SnakeGame.Database
{
    public class DatabaseHelper
    {
        private static string connectionString;
        private static string masterConnectionString;

        static DatabaseHelper()
        {
            try
            {
                // Set DataDirectory to app folder
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                AppDomain.CurrentDomain.SetData("DataDirectory", appPath);

                connectionString = ConfigurationManager.ConnectionStrings["SnakeGameDB"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException(
                        "Connection string 'SnakeGameDB' không được tìm thấy trong App.config.");
                }

                // Connection string to master database for creating new database
                masterConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30";

                // Tự động tạo database nếu chưa có
                InitializeDatabase();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database initialization error: {ex.Message}");
                throw new InvalidOperationException(
                    "Lỗi khởi tạo database: " + ex.Message, ex);
            }
        }

        private static void InitializeDatabase()
        {
            try
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyNguoiChoi.mdf");
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "QuanLyNguoiChoi_log.ldf");
                
                // Nếu database chưa tồn tại, tạo mới
                if (!File.Exists(dbPath))
                {
                    System.Diagnostics.Debug.WriteLine("Database không tồn tại. Đang tạo database mới...");
                    CreateDatabase(dbPath, logPath);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Database đã tồn tại tại: {dbPath}");
                    // Kiểm tra và tạo bảng nếu chưa có
                    EnsureTablesExist();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Initialize database error: {ex.Message}");
            }
        }

        private static void CreateDatabase(string dbPath, string logPath)
        {
            try
            {
                // Bước 1: Tạo database file bằng cách kết nối master
                using (SqlConnection conn = new SqlConnection(masterConnectionString))
                {
                    conn.Open();
                    
                    // Tạo database với file path cụ thể
                    string createDbQuery = $@"
                        CREATE DATABASE QuanLyNguoiChoi
                        ON PRIMARY (
                            NAME = QuanLyNguoiChoi_Data,
                            FILENAME = '{dbPath}',
                            SIZE = 10MB,
                            FILEGROWTH = 5MB
                        )
                        LOG ON (
                            NAME = QuanLyNguoiChoi_Log,
                            FILENAME = '{logPath}',
                            SIZE = 5MB,
                            FILEGROWTH = 5MB
                        )
                    ";

                    using (SqlCommand cmd = new SqlCommand(createDbQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    System.Diagnostics.Debug.WriteLine("Database file created successfully");
                }

                // Bước 2: Tạo bảng trong database mới
                CreateTables();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create database error: {ex.Message}");
                // Nếu database đã tồn tại, bỏ qua lối
                if (!ex.Message.Contains("already exists"))
                {
                    throw;
                }
                else
                {
                    // Database đã tồn tại, chỉ cần tạo bảng
                    CreateTables();
                }
            }
        }

        private static void CreateTables()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();

                    // Tạo bảng TAIKHOAN
                    string createTaiKhoanTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TAIKHOAN')
                        BEGIN
                            CREATE TABLE TAIKHOAN (
                                player_ID INT PRIMARY KEY IDENTITY(1,1),
                                username NVARCHAR(50) UNIQUE NOT NULL,
                                matkhau NVARCHAR(256) NOT NULL,
                                email NVARCHAR(255) UNIQUE NOT NULL,
                                JoinDate DATETIME DEFAULT GETDATE(),
                                HighestScore INT DEFAULT 0
                            );

                            -- Tạo tài khoản mặc định (username: admin, password: admin - đã hash SHA256)
                            INSERT INTO TAIKHOAN (username, matkhau, email, HighestScore)
                            VALUES ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'admin@snakegame.com', 0);
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(createTaiKhoanTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Tạo bảng SCORES
                    string createScoresTable = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SCORES')
                        BEGIN
                            CREATE TABLE SCORES (
                                score_ID INT PRIMARY KEY IDENTITY(1,1),
                                player_ID INT NOT NULL,
                                score INT NOT NULL,
                                AchievedAt DATETIME DEFAULT GETDATE(),
                                CONSTRAINT FK_SCORES_TAIKHOAN FOREIGN KEY (player_ID) 
                                    REFERENCES TAIKHOAN(player_ID)
                            );
                        END
                    ";

                    using (SqlCommand cmd = new SqlCommand(createScoresTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    System.Diagnostics.Debug.WriteLine("Database tables created successfully with default account (username: admin, password: admin)");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Create tables error: {ex.Message}");
                throw;
            }
        }

        private static void EnsureTablesExist()
        {
            try
            {
                CreateTables();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ensure tables exist error: {ex.Message}");
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Database connection error: {ex.Message}");
                System.Windows.Forms.MessageBox.Show(
                    $"Không thể kết nối database!\n\nLỗi: {ex.Message}\n\n" +
                    "Vui lòng kiểm tra:\n" +
                    "1. Visual Studio đã cài đặt SQL Server LocalDB\n" +
                    "2. Có quyền ghi file trong thư mục ứng dụng\n\n" +
                    "Để khắc phục, mở Command Prompt (Run as Admin) và chạy:\n" +
                    "sqllocaldb create MSSQLLocalDB\n" +
                    "sqllocaldb start MSSQLLocalDB",
                    "Lỗi kết nối Database",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
